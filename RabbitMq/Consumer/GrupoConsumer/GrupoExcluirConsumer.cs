using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using Ms_Compras.Database.Entidades;
using Ms_Compras.Database.Interfaces;

namespace Ms_Compras.RabbitMq.Consumer.GrupoConsumer
{
    public class GrupoExcluirConsumer : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IGenericRepository<Grupo> _genericRepository;
        private const string ExchangeName = "ExcluirGrupo";
        private IConfiguration _configuration;
        string QueueName = string.Empty;
        public GrupoExcluirConsumer(IGenericRepository<Grupo> genericRepository, IConfiguration configuration)
        {
            _configuration = configuration;
            _genericRepository = genericRepository;
            var factory = new ConnectionFactory
            {
                UserName = _configuration["RabbitMq:UserName"],
                Password = _configuration["RabbitMq:Password"],
                HostName = _configuration["RabbitMq:HostName"],
                Port = int.Parse(_configuration["RabbitMq:Port"])
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(ExchangeName, ExchangeType.Fanout);
            QueueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(QueueName, ExchangeName, "");
            _configuration = configuration;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var grupo = JsonSerializer.Deserialize<Grupo>(message);
                _genericRepository.ExcluirAsync(grupo);

                _channel.BasicAck(e.DeliveryTag, false);
            };

            _channel.BasicConsume(QueueName, false, consumer);

            return Task.CompletedTask;
        }
    }
}