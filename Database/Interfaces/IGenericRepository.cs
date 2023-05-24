using System.Linq.Expressions;
using Ms_Compras.Dtos;

namespace Ms_Compras.Database.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> AdicionarAsync(T objeto);
        Task<T> EditarAsync(T objeto);
        Task<T> GetById(Expression<Func<T, bool>> predicate);
        Task<string> ExcluirAsync(T objeto);
        Task<PaginacaoDto<T>> GetPaginacaoAsync(PaginacaoRequestDto paginacaoRequestDto, 
            Expression<Func<T, string>> predicate, 
            Expression<Func<T, bool>> predicateWhere,
            List<Expression<Func<T, object>>>? predicateInclude);
    }
}
