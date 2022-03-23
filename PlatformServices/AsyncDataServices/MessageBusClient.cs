using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PlatformServices.Dtos;
using RabbitMQ.Client;

namespace PlatformServices.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageBusClient(IConfiguration configuration)
        {
            _configuration = configuration;
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQHost"],
                Port = int.Parse(_configuration["RabbitMQPort"])
            };

            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutDown;

                Console.WriteLine($"--> Connect to messageBus");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not connect to the message bus: {ex.Message}");
            }
        }
        public void PublishNewPlatform(PlatformPublishedDto platformPublishedDto)
        {
            var message = JsonSerializer.Serialize(platformPublishedDto);

            if (_connection.IsOpen)
            {
                Console.WriteLine($"--> RabbitMQ Connectiion Open, sendding message ...");
                SendMessage(message);
            }
            else
            {
                Console.WriteLine($"--> RabbitMQ Connectiion Close ...");
            }
        }

        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "trigger",
                                  routingKey: "",
                                  basicProperties: null,
                                  body: body);
            Console.WriteLine($"--> we have send {message}");
        }

        public void Dispose()
        {
            Console.WriteLine("MessageBus Dispodes");
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }

        private void RabbitMQ_ConnectionShutDown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine($"--> RabbitMQ Connection Shutdown");
        }
    }
}