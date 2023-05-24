using Ms_Compras.Dtos.NotaFiscalDeEntrada;

namespace Ms_Compras.Services.Interfaces
{
    public interface INotaFiscalDeEntradaService
    {
        Task<List<UltimasComprasDto>> GetUltimasComprasPorPeriodo(FiltroUltimasCompras filtroUltimasCompras, Guid empresaId);
    }
}