using Ms_Compras.Database.Entidades;
using Ms_Compras.Dtos.FiltrosCompra;

namespace Ms_Compras.Database.Interfaces
{
    public interface IOrdemDeProducaoRepository : IGenericRepository<OrdemDeProducao>
    {
        Task<List<OrdemDeProducao>> GetOrdemDeProducaoFiltroNosItens(FiltroCompraConsumo filtroCompraDto);
        Task<List<OrdemDeProducao>> GetOrdemDeProducaoFiltroNoCabecalho(FiltroCompraConsumo filtroCompraDto);
    }
}