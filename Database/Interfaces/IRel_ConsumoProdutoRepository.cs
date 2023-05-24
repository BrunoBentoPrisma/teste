using Ms_Compras.Database.Entidades;
using Ms_Compras.Dtos;
using Ms_Compras.Dtos.Compra;

namespace Ms_Compras.Database.Interfaces
{
    public interface IRel_ConsumoProdutoRepository : IGenericRepository<Rel_ConsumoProduto>
    {
        Task<List<ItensCompraFiltroViewDto>> GetItensPorConsumo(FiltroCompraDto filtroCompraDto, List<string> gruposIds);
    }
}
