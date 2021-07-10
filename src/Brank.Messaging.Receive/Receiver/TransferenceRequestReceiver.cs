﻿using Bank.Messaging.Receive.Options;
using Bank.Transaction.Application.Services;
using Bank.Transfer.Domain.Core.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bank.Messaging.Receive.Receiver
{
    public class TransferenceRequestReceiver : BackgroundService
    {
        private IModel _channel;
        private IConnection _connection;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly string _hostname;
        private readonly int _port;
        private readonly string _queueName;
        private readonly string _username;
        private readonly string _password;

        public TransferenceRequestReceiver(IServiceScopeFactory scopeFactory,
                                            IOptions<RabbitMqConfiguration> rabbitMqOptions)
        {
            _scopeFactory = scopeFactory;
            _hostname = rabbitMqOptions.Value.Hostname;
            _port = rabbitMqOptions.Value.Port;
            _queueName = rabbitMqOptions.Value.QueueName;
            _username = rabbitMqOptions.Value.UserName;
            _password = rabbitMqOptions.Value.Password;
            InitializeRabbitMqListener();
        }

        private void InitializeRabbitMqListener()
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostname,
                Port = _port,
                UserName = _username,
                Password = _password
            };

            _connection = factory.CreateConnection();
            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
           
                stoppingToken.ThrowIfCancellationRequested();

                var consumer = new EventingBasicConsumer(_channel);
                consumer.Received += (ch, ea) =>
                {
                    var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                    var transferRequestedEvent = JsonConvert.DeserializeObject<TransferRequestedEvent>(content);

                    HandleMessage(transferRequestedEvent);

                    _channel.BasicAck(ea.DeliveryTag, false);
                };
                //consumer.Shutdown += OnConsumerShutdown;
                //consumer.Registered += OnConsumerRegistered;
                //consumer.Unregistered += OnConsumerUnregistered;
                //consumer.ConsumerCancelled += OnConsumerCancelled;

                _channel.BasicConsume(_queueName, false, consumer);

                return Task.CompletedTask;
        }

        private void HandleMessage(TransferRequestedEvent transference)
        {
            //enviar para a api PROCESS

            using (var scope = _scopeFactory.CreateScope())
            {
                var _transactionAppService = scope.ServiceProvider.GetRequiredService<ITransactionAppService>();
                _transactionAppService.ProccessTransferenceAsync(transference);

            }
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
