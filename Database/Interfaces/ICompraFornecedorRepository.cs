using Ms_Compras.Database.Entidades;
using Ms_Compras.Dtos.CompraFornecedor;

namespace Ms_Compras.Database.Interfaces
{
    public interface ICompraFornecedorRepository : IGenericRepository<CompraFornecedor>
    {
        Task<CompraFornecedor> GetByIdAsync(Guid id, Guid empresaId);
        Task<List<CompraFornecedor>> GetListByCompraIdAsync(Guid id, Guid empresaId);
        Task<List<CompraFornecedor>> ConsultarPedido(FiltroPedidoCompraFornecedor filtroPedido, Guid empresaId);
        Task<List<CompraFornecedor>> GetFiltroCotacaoCompras(FiltroCotacaoCompraDto filtroCotacaoCompraDto, Guid empresaId);
    }
}
