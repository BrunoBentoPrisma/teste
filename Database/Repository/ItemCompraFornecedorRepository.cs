using Ms_Compras.Context;
using Ms_Compras.Database.Entidades;
using Ms_Compras.Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ms_Compras.Database.Repository
{
    public class ItemCompraFornecedorRepository : GenericRepository<ItensCompraFornecedor>, IItemCompraFornecedorRepository
    {
        public async Task<ItensCompraFornecedor> GetItem(Guid id)
        {
            try
            {
                using(var data = new MsContext(_OptionsBuilder))
                {
                    return await data.ItensCompraFornecedor
                        .AsNoTracking()
                        .SingleOrDefaultAsync(x => x.Id == id && !x.Excluido);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}