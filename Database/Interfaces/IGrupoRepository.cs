using Ms_Compras.Database.Entidades;

namespace Ms_Compras.Database.Interfaces
{
    public interface IGrupoRepository : IGenericRepository<Grupo>
    {
        Task<Grupo> GetGrupoById(Guid id);
    }
}