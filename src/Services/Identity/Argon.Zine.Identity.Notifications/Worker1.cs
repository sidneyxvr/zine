using Argon.Zine.Identity.Notifications.Commands;
using Argon.Zine.Identity.Notifications.Handlers;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Argon.Zine.Identity.Notifications
{
    public class Worker1 : BackgroundService
    {
        private IModel? _channel;
        private IConnection? _connection;
        private readonly ILogger<Worker1> _logger;
        private readonly ConnectionFactory _connectionFactory;
        private readonly SendEmailResetPasswordHandler _handler;

        public Worker1(
            ILogger<Worker1> logger,
            ConnectionFactory connectionFactory,
            SendEmailResetPasswordHandler handler)
        {
            _logger = logger;
            _handler = handler;
            _connectionFactory = connectionFactory;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(
                queue: "SendEmailResetPassword",
                durable: false,
                exclusive: false,
                autoDelete: false);
            _logger.LogInformation($"Queue [SendEmailResetPassword] is waiting for messages.");

            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();

                var message = Encoding.UTF8.GetString(body);

                var command = JsonSerializer.Deserialize<SendEmailResetPasswordCommand>(message);
                _logger.LogInformation("command", command);
                try
                {
                    await _handler.HandleAsync(command!);

                    _channel!.BasicAck(ea.DeliveryTag, multiple: false);
                }
                catch (Exception ex)
                {
                    _channel!.BasicNack(ea.DeliveryTag, multiple: false, requeue: true);
                    _logger.LogError(ex.Message, ex);
                }
            };

            _channel.BasicConsume(
                queue: "SendEmailResetPassword",
                autoAck: false,
                consumer: consumer);

            await Task.CompletedTask;
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);
            _connection?.Close();
            _logger.LogInformation("RabbitMQ connection is closed.");
        }
    }
}