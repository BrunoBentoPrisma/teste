using Microsoft.EntityFrameworkCore;
using Ms_Compras.Context;
using Ms_Compras.Database.Entidades;
using Ms_Compras.Database.Interfaces;

namespace Ms_Compras.Database.Repository
{
    public class ConfigEstoqueFilialRepository : GenericRepository<ConfigEstoqueFilial>, IConfigEstoqueFilialRepository
    {
        public async Task<ConfigEstoqueFilial> GetConfigEstoqueFilialAsync(Guid empresaId, Guid produtoId)
        {
            try
            {
                using(var data = new MsContext(_OptionsBuilder))
                {
                    return await data.ConfigEstoqueFilial
                        .AsNoTracking()
                        .SingleOrDefaultAsync(x => x.EmpresaId == empresaId && x.ProdutoId == produtoId && !x.Excluido);
                    }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> GetControlaEstoqueMinimoMaximoPorFilial(Guid empresaId)
        {
            try
            {
                using(var data = new MsContext(_OptionsBuilder))
                {
                    var config = await data.ConfigEstoqueFilial
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.EmpresaId == empresaId);

                    if (config is null) return false;

                    return config.ControlaEstoqueMinimoMaximoPorFilial;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}