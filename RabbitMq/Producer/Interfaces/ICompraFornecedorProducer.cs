using Ms_Compras.Database.Entidades;

namespace Ms_Compras.RabbitMq.Producer.Interfaces
{
    public interface ICompraFornecedorProducer
    {
        void CompraFornecedorMessage(CompraFornecedor compraFornecedor, string ExchangeName);
    }
}