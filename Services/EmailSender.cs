﻿using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using Services.Interfaces;
using Services.Options;
using static Services.Interfaces.IEmailSender;

namespace Services;

public class EmailSender : IEmailSender
{
    private readonly EmailSenderOptions _configuration;
    private readonly ILogger<EmailSender> _logger;

    public EmailSender(IOptions<EmailSenderOptions> configuration, ILogger<EmailSender> logger)
    {
        _configuration = configuration.Value;
        _logger = logger;
    }

    public async Task SendEmailAsync(string email, EmailTemplate template, params object?[] parameters)
    {
        foreach (var parameter in parameters)
            _logger.LogInformation("Parameter: {0}", parameter);
        try
        {
            BodyBuilder builder = new()
            {
                HtmlBody = string.Format(await File.ReadAllTextAsync($"Templates\\{template.Filename}.html"),
                    parameters)
            };
            var emailMessage = new MimeMessage
            {
                Subject = template.Subject,
                Body = builder.ToMessageBody()
            };
            emailMessage.From.Add(new MailboxAddress(_configuration.Username, _configuration.From));
            emailMessage.To.Add(new MailboxAddress(string.Empty, email));

            using var client = new SmtpClient();
            await client.ConnectAsync(_configuration.SmptServer,
                int.Parse(_configuration.Port),
                true);
            await client.AuthenticateAsync(_configuration.From,
                _configuration.Password);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
            _logger.LogInformation("File '{file}' was sent on {email}", template.Filename, email);
        }
        catch (Exception ex)
        {
            _logger.LogError("{Message}", ex.Message);
        }
    }
}