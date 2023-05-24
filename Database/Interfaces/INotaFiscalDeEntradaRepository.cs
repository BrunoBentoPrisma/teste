using Ms_Compras.Database.Entidades;
using Ms_Compras.Dtos.NotaFiscalDeEntrada;

namespace Ms_Compras.Database.Interfaces
{
    public interface INotaFiscalDeEntradaRepository : IGenericRepository<NotaFiscalDeEntrada>
    {
        Task<NotaFiscalDeEntrada> GetUltimaCompra(Guid produtoId, Guid empresaId);
        Task<List<NotaFiscalDeEntrada>> GetUltimasComprasPorPeriodo(FiltroUltimasCompras filtroUltimasCompras, Guid empresaId);
    }
}