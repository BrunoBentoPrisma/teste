using Microsoft.EntityFrameworkCore;
using Ms_Compras.Context;
using Ms_Compras.Database.Entidades;
using Ms_Compras.Database.Interfaces;

namespace Ms_Compras.Database.Repository
{
    public class LoteRepository : GenericRepository<Lote>, ILoteRepository
    {
        public async Task<decimal> GetQuantidadeComprometida(Guid produtoId, Guid gurpoId, Guid empresaId)
        {
            try
            {
                using(var data = new MsContext(_OptionsBuilder))
                {
                    return await data.Lote
                    .Include(x => x.Produto)
                    .Where(x => !x.Excluido && x.EmpresaId == empresaId && x.ProdutoId == produtoId && x.Produto.GrupoId == gurpoId)
                    .SumAsync(x => x.QuantidadeComprometidaLote);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}