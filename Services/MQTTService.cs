/*
using BusinessLogic.Interfaces;
using DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Server;
using System.Globalization;
using DataBase;
using Microsoft.Extensions.Options;

namespace BusinessLogic
{
    public class MQTTService : IMQTTService
    {
        private readonly IConfiguration _configuration;
        private readonly IMqttClient _client;
        private readonly MqttClientOptions _options;

        public MQTTService(IConfiguration configuration)
        {
            _configuration = configuration;

            _options = new MqttClientOptionsBuilder().WithClientId("FarmTracker/Server")
                                                        .WithTcpServer("broker.emqx.io")
                                                        .Build();
            var factory = new MqttFactory();
            _client = factory.CreateMqttClient();

            ConfigureMqttClient();

            _client.ConnectAsync(_options).Wait();
        }

        private void ConfigureMqttClient()
        {
            _client.ConnectedAsync += HandleConnectedAsync;
            _client.ApplicationMessageReceivedAsync += HandleApplicationMessageReceivedAsync;
            _ = Task.Run(async () =>
            {
                while (true)
                {
                    try
                    {
                        if (!await _client.TryPingAsync())
                        {
                            await _client.ConnectAsync(_options);
                            Console.WriteLine("The MQTT client is connected.");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("The MQTT client can't connect.");
                    }
                    finally
                    {
                        await Task.Delay(TimeSpan.FromSeconds(5));
                    }
                }
            });
        }

        public async Task PublishAsync(string payload)
        {
            var message = new MqttApplicationMessageBuilder()
                          .WithTopic("FarmTracker")
                          .WithPayload(payload)
                          .Build();
            await _client.PublishAsync(message);
        }

        public async Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            string[] payload = eventArgs.ApplicationMessage.ConvertPayloadToString().Split(";");
            int waterLevel = (int)double.Parse(payload[0], CultureInfo.InvariantCulture);
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(_configuration.GetConnectionString("DefaultConnection")).Options;
            using var _context = new ApplicationDbContext(options);

            var drinker = _context.Drinkers.FirstOrDefault(x => x.SerialNumber == payload[1]);

            drinker!.WaterLevel = waterLevel;
            _context.Update(drinker);
            await _context.SaveChangesAsync();
        }

        public async Task HandleConnectedAsync(MqttClientConnectedEventArgs eventArgs)
        {
            Console.WriteLine("Mqtt connected");
            await _client.SubscribeAsync("FarmTracker/WaterLevel");
        }
    }
}
*/

