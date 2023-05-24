using System.Text;
using System.Text.Json;
using Ms_Compras.Database.Entidades;
using Ms_Compras.RabbitMq.Producer.Interfaces;
using RabbitMQ.Client;

namespace Ms_Compras.RabbitMq.Producer.Implementacao
{
    public class CompraFornecedorProducer : ICompraFornecedorProducer
    {
        private IConnection _connection;
        private IConfiguration _configuration;

        public CompraFornecedorProducer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void CompraFornecedorMessage(CompraFornecedor compra, string ExchangeName)
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

            channel.ExchangeDeclare(ExchangeName, ExchangeType.Fanout, durable: false);
            var message = JsonSerializer.Serialize(compra);
            var messageBytes = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: ExchangeName, routingKey: "", basicProperties: null, body: messageBytes);
        }
    }
}