using Argon.Zine.Identity.Notifications;
using Argon.Zine.Identity.Notifications.Commands;
using Argon.Zine.Identity.Notifications.Handlers;
using Argon.Zine.Identity.Notifications.Models;
using OpenTelemetry.Trace;
using RabbitMQ.Client;
using System.Net;
using System.Net.Mail;


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        IConfiguration configuration = context.Configuration;

        services.AddHostedService<Worker>();

        services.AddSingleton<SendEmailResetPasswordHandler>();
        services.AddSingleton<SendEmailConfirmationAccountHandler>();

        services.AddSingleton<ConnectionFactory>(_
            => new ConnectionFactory() { HostName = "rabbitmq", DispatchConsumersAsync = true });

        //the purpose of this implementation is study
        services.AddSingleton<Func<Type, object>>(provider
            => type =>
            {
                if (type == typeof(SendEmailConfirmationAccountCommand))
                {
                    return provider.GetRequiredService<SendEmailConfirmationAccountHandler>();
                }
                else if (type == typeof(SendEmailResetPasswordCommand))
                {
                    return provider.GetRequiredService<SendEmailResetPasswordHandler>();
                }

                throw new ArgumentException(nameof(type));
            });

        services.AddOpenTelemetryTracing(builder =>
        {
            builder.AddJaegerExporter(options => options.AgentHost = "jaeger")
                .AddSource("RabbitMQ")
                //.AddEntityFrameworkCoreInstrumentation(options => options.SetDbStatementForText = true)
                ;
        });

        services.AddSingleton<SendEmailConfirmationAccountHandler>();
        services.AddSingleton<SendEmailResetPasswordHandler>();

        var emailSenderSettingsSection = configuration.GetSection(nameof(EmailSenderSettings));
        var emailSenderSettings = emailSenderSettingsSection.Get<EmailSenderSettings>();
        services.AddFluentEmail(emailSenderSettings?.Email ?? "")
            .AddRazorRenderer()
            .AddSmtpSender(new SmtpClient
            {
                //Host = emailSenderSettings.Host,
                //Port = emailSenderSettings.Port,
                //EnableSsl = true,
                //UseDefaultCredentials = false,
                //Credentials = new NetworkCredential(emailSenderSettings.Email, emailSenderSettings.Password)
            });
    })
    .Build();

await host.RunAsync();
