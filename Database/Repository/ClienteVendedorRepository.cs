using Microsoft.EntityFrameworkCore;
using Ms_Compras.Context;
using Ms_Compras.Database.Entidades;
using Ms_Compras.Database.Interfaces;

namespace Ms_Compras.Database.Repository
{
    /// <summary>
    ///   Excluir este repository assim que for criado o Ms-Vendas e adicionar 
    ///   as entidades Cliente e Vendedor no Ms-Vendas
    /// </summary>
    public class ClienteVendedorRepository : GenericRepository<Cliente>, IClienteVendedorRepository
    {
        public async Task<List<Cliente>> GetClientes(Guid empresaId)
        {
            using(var data = new MsContext(_OptionsBuilder))
            {
                return await data.Cliente
                    .AsNoTracking()
                    .Take(1000)
                    .Skip(0)
                    .Where(x => !x.Excluido && x.EmpresaId == empresaId)
                    .ToListAsync(); 

            }
        }

        public async Task<List<Vendedor>> GetVendedores(Guid empresaId)
        {
            using (var data = new MsContext(_OptionsBuilder))
            {
                return await data.Vendedor
                    .AsNoTracking()
                    .Where(x => !x.Excluido && x.EmpresaId == empresaId)
                    .ToListAsync();

            }
        }
    }
}
