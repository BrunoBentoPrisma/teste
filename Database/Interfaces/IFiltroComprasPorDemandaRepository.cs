using Ms_Compras.Dtos;

namespace Ms_Compras.Database.Interfaces
{
    public interface IFiltroComprasPorDemandaRepository :IGenericRepository<ItensCompraFiltroViewDto>
    {
        Task<List<ItensCompraViewPorDemanda>> GetListaFiltroDemanda(Guid empresaId, DateTime dataInicial, DateTime dataFinal);
    }
}
