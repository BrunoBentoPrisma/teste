using Ms_Compras.Database.Entidades;
using Ms_Compras.Dtos;
using Ms_Compras.Dtos.FiltrosCompra;

namespace Ms_Compras.Database.Interfaces
{
    public interface IFaltasEncomendasRepository : IGenericRepository<FaltasEncomendas>
    {
        Task<FaltasEncomendas> GetByIdAsync(Guid id, Guid empresaId);
        Task<List<FaltasEncomendas>> GetAllAsync(Guid empresaId);
        Task<decimal> GetQuantidadeFaltaEncomenda(Guid produtoId, Guid empresaId);
        Task<List<FaltasEncomendas>> GetFiltroCompraPorPeriodoAsync(FiltroCompraFaltasEncomendas filtroCompraFaltasEncomendasDto);
    }
}
