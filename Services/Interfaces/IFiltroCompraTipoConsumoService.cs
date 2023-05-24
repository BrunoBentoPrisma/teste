using Ms_Compras.Dtos.Compra;
using Ms_Compras.Dtos;

namespace Ms_Compras.Services.Interfaces
{
    public interface IFiltroCompraTipoConsumoService
    {
        Task<List<ItensCompraFiltroViewDto>> GetFiltroItensCompra(FiltroCompraDto filtroCompraDto);
    }
}
