using Argon.Zine.Identity.Notifications.Commands;
using OpenTelemetry;
using OpenTelemetry.Context.Propagation;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Diagnostics;
using System.Text;

namespace Argon.Zine.Identity.Notifications
{
    internal class Worker : BackgroundService
    {
        private static readonly ActivitySource _activitySource = new("RabbitMQ");
        private static readonly TextMapPropagator _propagator = Propagators.DefaultTextMapPropagator;

        private IModel? _channel;
        private IConnection? _connection;
        private readonly ILogger<Worker> _logger;
        private readonly Func<Type, object> _handlerFactory;
        private readonly ConnectionFactory _connectionFactory;

        public Worker(
            ILogger<Worker> logger,
            Func<Type, object> handlerFactory,
            ConnectionFactory connectionFactory)
        {
            _logger = logger;
            _handlerFactory = handlerFactory;
            _connectionFactory = connectionFactory;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(
                queue: "IdentityNotification",
                durable: false,
                exclusive: false,
                autoDelete: false);

            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var parentContext = _propagator.Extract(
                    default,
                    ea.BasicProperties,
                    (props, key) =>
                        props.Headers.TryGetValue(key, out var value)
                            ? new[] { Encoding.UTF8.GetString((byte[])value) }
                            : Enumerable.Empty<string>()
                    );

                Baggage.Current = parentContext.Baggage;
                var activityName = $"{ea.RoutingKey} receive";
                using var activity = _activitySource.StartActivity(activityName, ActivityKind.Consumer, parentContext.ActivityContext);
                var body = ea.Body.ToArray();

                var message = Encoding.UTF8.GetString(body);

                activity?.SetTag("message", message);

                try
                {
                    var command = CommandFactory.Create(ea.BasicProperties.Type, message);

                    _logger.LogInformation("command", command);

                    var handler = _handlerFactory(command.GetType());

                    await Task.Delay(100);
                    //await (Task)handler.GetType().GetMethod("HandleAsync")!.Invoke(handler, new[] { command })!;

                    _channel!.BasicAck(ea.DeliveryTag, multiple: false);
                }
                catch (Exception ex)
                { 
                    _channel!.BasicNack(ea.DeliveryTag, multiple: false, requeue: true);
                    _logger.LogError(ex.Message, ex);
                }
            };

            _channel.BasicConsume(
                queue: "IdentityNotification",
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