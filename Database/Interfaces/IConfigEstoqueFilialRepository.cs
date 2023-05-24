using Ms_Compras.Database.Entidades;

namespace Ms_Compras.Database.Interfaces
{
    public interface IConfigEstoqueFilialRepository : IGenericRepository<ConfigEstoqueFilial>
    {
        Task<ConfigEstoqueFilial> GetConfigEstoqueFilialAsync(Guid empresaId, Guid produtoId);
        Task<bool> GetControlaEstoqueMinimoMaximoPorFilial(Guid empresaId);
    }
}