using Ms_Compras.Database.Entidades;
using Ms_Compras.Dtos.FiltrosCompra;

namespace Ms_Compras.Database.Interfaces
{
    public interface IProdutoRepository : IGenericRepository<Produto>
    {
        Task<Produto> GetProdutoByIntegracaoId(string integracaoId);
        Task<Produto> GetProdutoByIdAsync(Guid id, Guid empresaId);
        Task<List<Produto>> GetFiltroCompraEstoqueMinimoMaximo(FiltroCompraEstoqueMinimoEstoqueMaximo filtroCompraEstoqueMinimoEstoqueMaximo);
    }
}
