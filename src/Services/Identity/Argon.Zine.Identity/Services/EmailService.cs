using Argon.Zine.Commom.DomainObjects;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Argon.Zine.Identity.Services;

public class EmailService : IEmailService
{
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