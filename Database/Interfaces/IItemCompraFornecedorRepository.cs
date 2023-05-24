using Ms_Compras.Database.Entidades;

namespace Ms_Compras.Database.Interfaces
{
    public interface IItemCompraFornecedorRepository : IGenericRepository<ItensCompraFornecedor>
    {
        Task<ItensCompraFornecedor> GetItem(Guid id);
    }
}