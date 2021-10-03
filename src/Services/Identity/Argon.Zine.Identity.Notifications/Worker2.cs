using Argon.Zine.Identity.Notifications.Commands;
using Argon.Zine.Identity.Notifications.Handlers;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Argon.Zine.Identity.Notifications
{
    public class Worker2 : BackgroundService
    {
        private readonly ILogger<Worker2> _logger;
        private readonly ConnectionFactory _connectionFactory;
        private readonly SendEmailConfirmationAccountHandler _handler;

        public Worker2(
            ILogger<Worker2> logger, 
            ConnectionFactory connectionFactory, 
            SendEmailConfirmationAccountHandler handler)
        {
            _logger = logger;
            _handler = handler;
            _connectionFactory = connectionFactory;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var connection = _connectionFactory.CreateConnection()) 
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: "SendEmailConfirmationAccount",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var consumer = new AsyncEventingBasicConsumer(channel);
                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();

                    var message = Encoding.UTF8.GetString(body);

                    var command = JsonSerializer.Deserialize<SendEmailConfirmationAccountCommand>(message);

                    await _handler.HandleAsync(command!);

                    channel.BasicAck(ea.DeliveryTag, multiple: false);
                };

                channel.BasicConsume(queue: "SendEmailConfirmationAccount",
                                     autoAck: false,
                                     consumer: consumer);
            }

            return Task.CompletedTask;
        }
    }
}