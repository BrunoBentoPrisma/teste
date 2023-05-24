using Ms_Compras.Database.Entidades;
using Ms_Compras.Dtos.FiltrosCompra;

namespace Ms_Compras.Database.Interfaces
{
    public interface IEmbalagemOrdemDeProducaoRepository : IGenericRepository<EmbalagemOrdemDeProducao>
    {
        Task<List<EmbalagemOrdemDeProducao>> GetEmbalagemOrdemDeProducaoFiltroNoCabecalho(FiltroCompraConsumo filtroCompraDto, Guid numeroOrdemDeProducao);
        Task<List<EmbalagemOrdemDeProducao>> GetEmbalagemOrdemDeProducaoFiltroNosItens(FiltroCompraConsumo filtroCompraDto, Guid numeroOrdemDeProducao);
    }
}