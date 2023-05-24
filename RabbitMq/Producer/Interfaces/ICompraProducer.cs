using Ms_Compras.Database.Entidades;

namespace Ms_Compras.RabbitMq.Producer.Interfaces
{
    public interface ICompraProducer
    {
        void CompraMessage(Compra compra, string ExchangeName);
    }
}