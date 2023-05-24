using Ms_Compras.Database.Entidades;

namespace Ms_Compras.Database.Interfaces
{
    public interface ICompraRepository : IGenericRepository<Compra>
    {
        Task<Compra> GetByIdAsync(Guid id, Guid empresaId);
        Task<List<Compra>> GetAllAsync(Guid empresaId);
    }
}
