using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using Ms_Compras.Context;
using Ms_Compras.Database.Interfaces;
using Ms_Compras.Dtos;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace Ms_Compras.Database.Repository
{
    public class GenericRepository<T> : IGenericRepository<T>, IDisposable where T : class
    {
        public readonly DbContextOptions<MsContext> _OptionsBuilder;
        public GenericRepository()
        {
            _OptionsBuilder = new DbContextOptions<MsContext>();
        }
        public async Task<T> AdicionarAsync(T objeto)
        {
            using (var data = new MsContext(_OptionsBuilder))
            {
                try
                {
                    await data.Set<T>().AddAsync(objeto);
                    await data.SaveChangesAsync();
                    return objeto;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Ocorreu um erro ao adicionar : {ex.Message}");
                }

            }
        }

        public async Task<T> EditarAsync(T objeto)
        {
            try
            {
                using (var data = new MsContext(_OptionsBuilder))
                {
                    data.Set<T>().Update(objeto);
                    data.Entry(objeto).Property("DataDeCadastro").IsModified = false;
                    data.Entry(objeto).Property("EmpresaId").IsModified = false;
                    data.Entry(objeto).Property("NomeCriador").IsModified = false;
                    data.Entry(objeto).Property("IntegracaoId").IsModified = false;
                    await data.SaveChangesAsync();
                    return objeto;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocorreu um erro ao editar :{ex.Message}");
            }
        }
        public async Task<string> ExcluirAsync(T objeto)
        {
            var message = string.Empty;
            try
            {
                using (var data = new MsContext(_OptionsBuilder))
                {
                    data.Set<T>().Update(objeto);
                    data.Entry(objeto).Property("DataDeCadastro").IsModified = false;
                    data.Entry(objeto).Property("NomeCriador").IsModified = false;
                    data.Entry(objeto).Property("EmpresaId").IsModified = false;
                    data.Entry(objeto).Property("DataDeAlteracao").IsModified = false;
                    data.Entry(objeto).Property("NomeEditor").IsModified = false;
                    data.Entry(objeto).Property("IntegracaoId").IsModified = false;
                    await data.SaveChangesAsync();
                    message = "Registro excluído com sucesso !";
                }
            }
            catch (DbUpdateException exDb)
            {
                var innerException = exDb.InnerException;
                while (innerException != null)
                {

                    if (innerException is Npgsql.PostgresException pgEx && pgEx.SqlState == "23503")
                    {
                        throw new Exception($"Não é possível excluir o registro porque há registros relacionados na tabela : {pgEx.TableName}", pgEx);
                    }
                    else
                    {
                        throw new Exception($"Ocorreu um erro interno : {innerException.Message}", innerException);
                    }

                }

            }

            return message;
        }
        public async Task<T> GetById(Expression<Func<T, bool>> predicate)
        {
            using (var data = new MsContext(_OptionsBuilder))
            {
                try
                {
                    return await data.Set<T>().FirstOrDefaultAsync(predicate);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Ocorreu um erro ao retornar o objeto por id : {ex.Message}");
                }

            }
        }

        #region Disposed
        bool disposed = false;
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
            }

            disposed = true;
        }
        #endregion
        public async Task<PaginacaoDto<T>> GetPaginacaoAsync(PaginacaoRequestDto paginacaoRequestDto, 
            Expression<Func<T, string>> predicate, 
            Expression<Func<T, bool>> predicateWhere,
            List<Expression<Func<T, object>>>? predicateInclude)
        {
            var paginacao = new PaginacaoDto<T>();

            try
            {
                using (var data = new MsContext(_OptionsBuilder))
                {
                    var total = await data.Set<T>().Where(predicateWhere).CountAsync();

                    var totalPages = (int)Math.Ceiling(total / double.Parse(paginacaoRequestDto.PageSize.ToString()));
                    var page = Math.Min(Math.Max(1, paginacaoRequestDto.Page), totalPages);
                    paginacaoRequestDto.Page = page >= 1 ? page : 1;

                    var query = data.Set<T>().AsNoTracking().Where(predicateWhere);

                    if (predicateInclude is not null)
                    {
                        predicateInclude.ForEach(predicate =>
                        {
                            query = query.Include(predicate);
                        });
                    }

                    query = paginacaoRequestDto.Asc ? query.OrderBy(predicate) : query.OrderByDescending(predicate);

                    paginacao.Values = await query.Skip((paginacaoRequestDto.Page - 1) * paginacaoRequestDto.PageSize)
                        .Take(paginacaoRequestDto.PageSize)
                        .ToListAsync();


                    //if (predicateInclude is not null)
                    //{
                    //    paginacao.Values = paginacaoRequestDto.Asc ? await data.Set<T>()
                    //        .AsNoTracking()
                    //        .Include(predicateInclude)
                    //        .Where(predicateWhere)
                    //        .OrderBy(predicate)
                    //        .Skip((paginacaoRequestDto.Page - 1) * paginacaoRequestDto.PageSize)
                    //        .Take(paginacaoRequestDto.PageSize)
                    //        .ToListAsync()
                    //            :
                    //        await data.Set<T>()
                    //        .AsNoTracking()
                    //        .Include(predicateInclude)
                    //        .Where(predicateWhere)
                    //        .OrderByDescending(predicate)
                    //        .Skip((paginacaoRequestDto.Page - 1) * paginacaoRequestDto.PageSize)
                    //        .Take(paginacaoRequestDto.PageSize)
                    //        .ToListAsync();
                    //}
                    //else
                    //{
                    //    paginacao.Values = paginacaoRequestDto.Asc ? await data.Set<T>()
                    //        .AsNoTracking()
                    //        .Where(predicateWhere)
                    //        .OrderBy(predicate)
                    //        .Skip((paginacaoRequestDto.Page - 1) * paginacaoRequestDto.PageSize)
                    //        .Take(paginacaoRequestDto.PageSize)
                    //        .ToListAsync()
                    //            :
                    //        await data.Set<T>()
                    //        .AsNoTracking()
                    //        .Where(predicateWhere)
                    //        .OrderByDescending(predicate)
                    //        .Skip((paginacaoRequestDto.Page - 1) * paginacaoRequestDto.PageSize)
                    //        .Take(paginacaoRequestDto.PageSize)
                    //        .ToListAsync();
                    //}

                    paginacao.PageCount = totalPages;

                    return paginacao;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }        
    }
}
