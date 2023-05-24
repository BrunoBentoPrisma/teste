using Microsoft.EntityFrameworkCore;
using Ms_Compras.Context;
using Ms_Compras.Database.Entidades;
using Ms_Compras.Database.Interfaces;


namespace Ms_Compras.Database.Repository
{
    public class MovimentoProdutoRepository : GenericRepository<MovimentoProduto>, IMovimentoProdutoRepository
    {
        public async Task<decimal> GetQuantidadeSaldo(Guid produtoId, Guid empresaId)
        {
            try
            {
                using(var data = new MsContext(_OptionsBuilder))
                {
                    var movimento = await data.MovimentoProduto
                        .OrderByDescending(x => x.DataDeCadastro)
                        .FirstOrDefaultAsync(x => !x.Excluido && x.EmpresaId == empresaId && x.ProdutoId == produtoId);

                    if (movimento is null) return 0;

                    return movimento.Saldo;
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}