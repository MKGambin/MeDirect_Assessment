using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using static CommonLibrary.Models.EnumsHelper;

namespace CommonLibrary.Models
{
    public class RabbitMQHelper
    {
        // Docker Setup Instructions:
        // 1. Check Docker version: `docker --version`
        // 2. Start RabbitMQ container: `docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:management`
        // 3. Check if RabbitMQ is running: `docker ps`
        // 4. Access RabbitMQ management UI at: http://localhost:15672/
        //    Login: Username: guest, Password: guest

        private readonly string _rabbitMqHost = "";

        private readonly ConnectionFactory _connectionFactory;

        public QueueType _queueType { get; private set; }

        private IConnection? _connection;

        private IChannel? _channel;

        public RabbitMQHelper(IConfiguration configuration, QueueType queueType)
        {
            _rabbitMqHost = configuration["RabbitMQ:Host"] ?? "";

            _connectionFactory = new ConnectionFactory()
            {
                HostName = _rabbitMqHost,
            };

            _queueType = queueType;
        }

        public async Task InitializeAsync()
        {
            _connection = await _connectionFactory.CreateConnectionAsync();

            _channel = await _connection.CreateChannelAsync();

            await _channel.QueueDeclareAsync(
                queue: _queueType.GetQueueName(),
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );
        }

        public async Task PushMessageAsync(string message)
        {
            if (_connection == null) throw new InvalidOperationException();
            if (_channel == null) throw new InvalidOperationException();

            await _channel.BasicPublishAsync(
                exchange: "",
                routingKey: _queueType.GetQueueName(),
                mandatory: false,
                basicProperties: new BasicProperties()
                {
                    Persistent = true,
                },
                body: Encoding.UTF8.GetBytes(message)
            );
        }

        public async Task StartListeningAsync(Func<string, Task> messageHandler)
        {
            if (_connection == null) throw new InvalidOperationException();
            if (_channel == null) throw new InvalidOperationException();

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                await messageHandler(message);

                await Task.Yield();
            };

            await _channel.BasicConsumeAsync(
                queue: _queueType.GetQueueName(),
                autoAck: true,
                consumer: consumer
            );
        }
    }
}
