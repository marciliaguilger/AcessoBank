using Bank.Transfer.Domain.Core.Events;
using Bank.Transfer.Domain.Options;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bank.Transfer.Application.Events
{
    public class TransferenceEventHandler : INotificationHandler<TransferRequestedEvent>
    {
        
        private readonly string _hostname;
        private readonly string _password;
        private readonly string _queueName;
        private readonly string _username;
        private readonly int _port;
        private IConnection _connection;

        public TransferenceEventHandler(IOptions<RabbitMqConfiguration> rabbitMqOptions)
        {
            _queueName = rabbitMqOptions.Value.QueueName;
            _hostname = rabbitMqOptions.Value.Hostname;
            _username = rabbitMqOptions.Value.UserName;
            _password = rabbitMqOptions.Value.Password;
            _port = rabbitMqOptions.Value.Port;

            CreateConnection();
        }
        public Task Handle(TransferRequestedEvent notification, CancellationToken cancellationToken)
        {
            if (ConnectionExists())
            {
                using (var channel = _connection.CreateModel())
                {
                    channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

                    var json = JsonConvert.SerializeObject(notification);
                    var body = Encoding.UTF8.GetBytes(json);

                    channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
                }
            }
            return Task.CompletedTask;
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostname,
                    Port = _port,
                    UserName = _username,
                    Password = _password
                };
                _connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {
                //GERAR LOG
                Console.WriteLine($"Could not create connection: {ex.Message}");
            }
        }

        private bool ConnectionExists()
        {
            if (_connection != null)
            {
                return true;
            }

            CreateConnection();

            return _connection != null;
        }
    }
}
