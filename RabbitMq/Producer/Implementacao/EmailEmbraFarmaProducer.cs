using Ms_Compras.RabbitMq.Producer.Interfaces;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using Ms_Compras.Dtos;

namespace Ms_Compras.RabbitMq.Producer.Implementacao
{
    public class EmailEmbraFarmaProducer : IEmailEmbraFarmaProducer
    {
        private IConnection _connection;
        private IConfiguration _configuration;

        public EmailEmbraFarmaProducer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void EnviarEmail(MessageEmailDto messageEmailDto, string exchangeName)
        {
            var factory = new ConnectionFactory
            {
                UserName = _configuration["RabbitMq:UserName"],
                Password = _configuration["RabbitMq:Password"],
                HostName = _configuration["RabbitMq:HostName"],
                Port = int.Parse(_configuration["RabbitMq:Port"])
            };

            _connection = factory.CreateConnection();

            using var channel = _connection.CreateModel();

            channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout, durable: false);
            var message = JsonSerializer.Serialize(messageEmailDto);
            var messageBytes = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: exchangeName, routingKey: "", basicProperties: null, body: messageBytes);
        }
    }
}
