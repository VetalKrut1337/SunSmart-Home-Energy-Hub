using System.Net.Http.Json;
using DataBase;
using Microsoft.Extensions.DependencyInjection;

namespace Services;

public class IoTService : IIoTService
{
    private readonly HttpClient client = new();
    private readonly Random random = new();
    private readonly Dictionary<int, Task> Tasks = new();
    private readonly Dictionary<int, CancellationTokenSource> Tokens = new();
    private readonly IServiceProvider _serviceProvider;

    public IoTService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void On(int id)
    {
        List<int> ids;
        using (var scope = _serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            ids = context.Panels.Where(x => x.InstallationId == id).Select(x => x.Id).ToList();
        }
        
        CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        CancellationToken token = cancelTokenSource.Token;
        async void Action()
        {
            while (!token.IsCancellationRequested)
            {
                await Task.Delay(15000);
                var timeinfo = DateTime.Now;
                var list = new List<PanelReportRequest>();
                list.AddRange(ids.Select(t => new PanelReportRequest
                {
                    Temperature = random.Next(70, 90),
                    Intensity = random.Next(100, 150),
                    Voltage = random.Next(110, 220),
                    Current = random.Next(5, 10),
                    Status = true,
                    PanelId = t
                }));
                var avg = list.Average(x => x.Voltage * x.Current);
                var max = list.Max(x => x.Voltage * x.Current);

                using var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7297/api/InstallationReport");
                var contentReqest = new InstallationReportRequest
                {
                    Timestamp = DateTime.UtcNow,
                    TiltAngle = ((timeinfo.Hour - 4) * 60 + timeinfo.Minute) * 0.2,
                    Efficiency = avg / max,
                    InstallationId = id,
                    PanelReports = list
                };
                request.Content = JsonContent.Create(contentReqest);

                using var response = await client.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(response.StatusCode);
                Console.WriteLine(content);
            }
        }

        var task = new Task(Action, token);
        task.Start();
        Tasks.Add(id, task);
        Tokens.Add(id, cancelTokenSource);
    }

    public void Off(int id)
    {
        Tokens[id].Cancel();
        Tasks.Remove(id);
        Tokens.Remove(id);
    }

    private class InstallationReportRequest
    {
        public DateTime Timestamp { get; set; }
        public double TiltAngle { get; set; }
        public double Efficiency { get; set; }
        public int InstallationId { get; set; }
        public List<PanelReportRequest>? PanelReports { get; set; }
    }

    private class PanelReportRequest
    {
        public double Temperature { get; set; }
        public double Intensity { get; set; }
        public double Voltage { get; set; }
        public double Current { get; set; }
        public bool Status { get; set; }
        public int PanelId { get; set; }
    }
}