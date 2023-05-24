using Ms_Compras.Dtos.Compra;
using Ms_Compras.Dtos;

namespace Ms_Compras.Services.Interfaces
{
    public interface IFiltroCompraTipoEstoqueMinimoService
    {
        Task<List<ItensCompraFiltroViewDto>> GetFiltroItensCompra(FiltroCompraDto filtroCompraDto);
    }
}
