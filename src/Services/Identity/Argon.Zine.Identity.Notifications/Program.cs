using Argon.Zine.Identity.Notifications;
using Argon.Zine.Identity.Notifications.Handlers;
using Argon.Zine.Identity.Notifications.Models;
using RabbitMQ.Client;
using System.Net;
using System.Net.Mail;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        IConfiguration configuration = context.Configuration;

        services.AddHostedService<Worker1>();
        services.AddHostedService<Worker2>();

        services.AddSingleton<SendEmailResetPasswordHandler>();
        services.AddSingleton<SendEmailConfirmationAccountHandler>();

        services.AddSingleton<ConnectionFactory>(_ 
            => new ConnectionFactory() { HostName = "localhost", DispatchConsumersAsync = true });

        var emailSenderSettingsSection = configuration.GetSection(nameof(EmailSenderSettings));
        var emailSenderSettings = emailSenderSettingsSection.Get<EmailSenderSettings>();
        services.AddFluentEmail(emailSenderSettings.Email)
            .AddRazorRenderer()
            .AddSmtpSender(new SmtpClient
            {
                Host = emailSenderSettings.Host,
                Port = emailSenderSettings.Port,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(emailSenderSettings.Email, emailSenderSettings.Password)
            });
    })
    .Build();

await host.RunAsync();
