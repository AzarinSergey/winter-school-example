using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Svc.Implementation.Model;

namespace Svc.Implementation
{
    public class RabbitMqReceiverHostedService : BackgroundService
    {
        private readonly ServiceDbContext _db;
        private IConnection _connection;
        private IModel _channel;

        public RabbitMqReceiverHostedService(ServiceDbContext db)
        {
            _db = db;
            InitRabbitMq();
        }

        private void InitRabbitMq()
        {
            var factory = new ConnectionFactory { UserName = "guest", Password = "guest", HostName = "example-rabbit", Port = 5672, VirtualHost = "example_vhost" };

            _connection = factory.CreateConnection();

            _channel = _connection.CreateModel();

            _channel.QueueDeclare("example", false, false, false, null);

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body);

                await HandleMessage(content);
                _channel.BasicAck(ea.DeliveryTag, false);
            };

            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

            _channel.BasicConsume("example", false, consumer);

            return Task.CompletedTask;
        }

        private async Task HandleMessage(string content)
        {
            Console.WriteLine($"consumer received {content}");

            await new MessageProcessorExample(_db)
                .Process(content);
        }

        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e)
        {
            Console.WriteLine("ConsumerConsumerCancelled");
        }

        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e)
        {
            Console.WriteLine("ConsumerUnregistered");
        }

        private void OnConsumerRegistered(object sender, ConsumerEventArgs e)
        {
            Console.WriteLine("ConsumerRegistered");
        }

        private void OnConsumerShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("ConsumerShutdown");
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("RabbitMQ_ConnectionShutdown");
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
