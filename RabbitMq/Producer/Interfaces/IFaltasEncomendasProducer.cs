using Ms_Compras.Database.Entidades;

namespace Ms_Compras.RabbitMq.Producer.Interfaces
{
    public interface IFaltasEncomendasProducer
    {
        void FaltasEncomendasMessage(FaltasEncomendas faltasEncomendas, string ExchangeName);
    }
}