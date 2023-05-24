using Ms_Compras.Database.Entidades;

namespace Ms_Compras.Database.Interfaces
{
    public interface IClienteVendedorRepository : IGenericRepository<Cliente>
    {
        Task<List<Cliente>> GetClientes(Guid empresaId);
        Task<List<Vendedor>> GetVendedores(Guid empresaId);
    }
}
