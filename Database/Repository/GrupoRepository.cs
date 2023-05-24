using Microsoft.EntityFrameworkCore;
using Ms_Compras.Context;
using Ms_Compras.Database.Entidades;
using Ms_Compras.Database.Interfaces;

namespace Ms_Compras.Database.Repository
{
    public class GrupoRepository : GenericRepository<Grupo>, IGrupoRepository
    {
        public async Task<Grupo> GetGrupoById(Guid id)
        {
            try
            {
                using(var data = new MsContext(_OptionsBuilder))
                {
                    return await data.Grupo
                        .SingleOrDefaultAsync(x => !x.Excluido && x.Id == id);
                        
                }   
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}