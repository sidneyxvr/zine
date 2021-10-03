using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Argon.Zine.Identity.Services
{
    public class EmailService : IEmailService
    {
        private readonly IModel _channel;

        public EmailService(IModel channel) 
            => _channel = channel;

        public Task SendEmailConfirmationAccountAsync(string? to, string? emailConfirmationToken)
        {
            if (string.IsNullOrWhiteSpace(to) ||
                !Core.DomainObjects.Email.IsValid(to) ||
                string.IsNullOrWhiteSpace(emailConfirmationToken))
            {
                return Task.CompletedTask;
            }

            var model = new { Token = emailConfirmationToken };

            return Task.CompletedTask;
        }

        public Task SendEmailResetPasswordAsync(string? to, string? resetPasswordToken)
        {
            if (string.IsNullOrWhiteSpace(to) ||
                !Core.DomainObjects.Email.IsValid(to) ||
                string.IsNullOrWhiteSpace(resetPasswordToken))
            {
                return Task.CompletedTask;
            }

            var model = new { Email = to, Token = resetPasswordToken };

            _channel.QueueDeclare(
                queue: "SendEmailResetPassword",
                durable: false,
                exclusive: false,
                autoDelete: false);

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(model));

            _channel.BasicPublish(
                exchange: "",
                routingKey: "SendEmailResetPassword",
                basicProperties: null,
                body: body);

            return Task.CompletedTask;
        }
    }
}
