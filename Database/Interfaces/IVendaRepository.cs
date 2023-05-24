using Ms_Compras.Database.Entidades;
using Ms_Compras.Dtos.FiltrosCompra;

namespace Ms_Compras.Database.Interfaces
{
    public interface IVendaRepository : IGenericRepository<Venda>
    {
        Task<List<Venda>> GetFiltroCompra(FiltroCompraConsumo filtroCompraDto);
        Task<List<ItensVenda>> GetFiltroCompraTipoVenda(FiltroCompraVenda filtroCompraPorVenda);
    }
}