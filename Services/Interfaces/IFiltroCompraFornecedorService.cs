using Ms_Compras.Dtos.CompraFornecedor;

namespace Ms_Compras.Services.Interfaces
{
    public interface IFiltroCompraFornecedorService
    {
        //Task<List<UltimasComprasDto>> GetUltimasCompras(FiltroUltimasCompras filtroUltimasCompras, Guid empresaId);
        Task<List<CompraFornecedorViewDto>> GetFiltroCotacaoCompra(FiltroCotacaoCompraDto filtroCotacaoCompraDto, Guid empresaId);
        Task<List<CompraFornecedorViewDto>> ConsultarPedidoCompraFornecedor(FiltroPedidoCompraFornecedor filtroPedido, Guid empresaId);
    }
}
