using Ms_Compras.Database.Entidades;

namespace Ms_Compras.Database.Interfaces
{
    public interface IMovimentoProdutoRepository : IGenericRepository<MovimentoProduto>
    {
        Task<decimal> GetQuantidadeSaldo(Guid produtoId, Guid empresaId);
    }
}