using Argon.Zine.Commom.DomainObjects;
using OpenTelemetry.Context.Propagation;
using RabbitMQ.Client;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using OpenTelemetry;

namespace Argon.Zine.Identity.Services;

public class EmailService : IEmailService
{
    private static readonly ActivitySource _activitySource = new("RabbitMQ");
    private static readonly TextMapPropagator _propagator = Propagators.DefaultTextMapPropagator;

    private readonly IModel _channel;

    public EmailService(IModel channel)
        => _channel = channel;

    public Task SendEmailConfirmationAccountAsync(string? to, string? emailConfirmationToken)
    {
        if (string.IsNullOrWhiteSpace(to) ||
            !Email.IsValid(to) ||
            string.IsNullOrWhiteSpace(emailConfirmationToken))
        {
            return Task.CompletedTask;
        }

        var model = new { Token = emailConfirmationToken };

        _channel.QueueDeclare(
            queue: "IdentityNotification",
            durable: false,
            exclusive: false,
            autoDelete: false);

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(model));
        var basicProperties = _channel.CreateBasicProperties();

        using var activity = _activitySource.StartActivity(ActivityKind.Consumer);

        var contextToInject = activity is not null
            ? activity.Context
            : Activity.Current!.Context;

        _propagator.Inject(new PropagationContext(contextToInject, Baggage.Current), 
            basicProperties, 
            (props, key, value) =>
            {
                props.Headers ??= new Dictionary<string, object>();
                props.Headers[key] = value;
            });

        basicProperties.Type = "SendEmailConfirmationAccount";
        _channel.BasicPublish(
            exchange: "",
            routingKey: "IdentityNotification",
            basicProperties: basicProperties,
            body: body);

        return Task.CompletedTask;
    }

    public Task SendEmailResetPasswordAsync(string? to, string? resetPasswordToken)
    {
        if (string.IsNullOrWhiteSpace(to) ||
            !Email.IsValid(to) ||
            string.IsNullOrWhiteSpace(resetPasswordToken))
        {
            return Task.CompletedTask;
        }

        var model = new { Email = to, Token = resetPasswordToken };

        _channel.QueueDeclare(
            queue: "IdentityNotification",
            durable: false,
            exclusive: false,
            autoDelete: false);

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(model));

        var basicProperties = _channel.CreateBasicProperties();
        basicProperties.Type = "SendEmailResetPassword";
        _channel.BasicPublish(
            exchange: "",
            routingKey: "IdentityNotification",
            basicProperties: basicProperties,
            body: body);

        return Task.CompletedTask;
    }
}