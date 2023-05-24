using Ms_Compras.Dtos;

namespace Ms_Compras.RabbitMq.Producer.Interfaces
{
    public interface IEmailEmbraFarmaProducer
    {
        void EnviarEmail(MessageEmailDto messageEmailDto, string exchangeName); 
    }
}
