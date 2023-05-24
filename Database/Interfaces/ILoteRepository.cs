using Ms_Compras.Database.Entidades;

namespace Ms_Compras.Database.Interfaces
{
    public interface ILoteRepository : IGenericRepository<Lote>
    {
        Task<decimal> GetQuantidadeComprometida(Guid produtoId, Guid gurpoId, Guid empresaId);
    }
}