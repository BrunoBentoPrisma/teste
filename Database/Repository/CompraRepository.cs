using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ms_Compras.Context;
using Ms_Compras.Database.Entidades;
using Ms_Compras.Database.Interfaces;

namespace Ms_Compras.Database.Repository
{
    public class CompraRepository : GenericRepository<Compra>, ICompraRepository
    {
        private readonly IMapper _mapper;

        public CompraRepository(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<List<Compra>> GetAllAsync(Guid empresaId)
        {
            try
            {
                using (var context = new MsContext(this._OptionsBuilder))
                {
                    return await context.Compra
                        .Include(compra => compra.ItensCompras)
                            .ThenInclude(item => item.Produto)
                                .ThenInclude(produto => produto.Grupo)
                        .Where(x => x.EmpresaId == empresaId && !x.Excluido)
                        .ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Compra> GetByIdAsync(Guid id, Guid empresaId)
        {
            try
            {
                using (var context = new MsContext(this._OptionsBuilder))
                {
                    return await context.Compra
                        .Include(compra => compra.ItensCompras)
                            .ThenInclude(item => item.Produto)
                                .ThenInclude(produto => produto.Grupo)
                        .FirstOrDefaultAsync(compra => compra.Id == id && compra.EmpresaId == empresaId && !compra.Excluido);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // public async Task<ItensCompra> GetUltimaCompraItensCompra(ItensCompra itemCompra, Guid empresaId)
        // {
        //     try
        //     {

        //         using (var data = new MsComprasContext(_OptionsBuilder))
        //         {
        //             var nfEntrada = await data.NotaFiscalDeEntrada
        //                 .Include(x => x.ItensNotaFiscalDeEntrada)
        //                 .Include(x => x.Fornecedor)
        //                 .OrderByDescending(x => x.DataDeCadastro)
        //                 .FirstOrDefaultAsync(x => x.EmpresaId == empresaId &&
        //                         x.ItensNotaFiscalDeEntrada.Any(x => x.ProdutoId == itemCompra.ProdutoId) &&
        //                         x.DataDeCadastro <= DateTime.Now);

        //             itemCompra.Produto.Fornecedor = nfEntrada?.Fornecedor;
        //         }

        //         return itemCompra;

        //     }
        //     catch (Exception ex)
        //     {
        //         throw new Exception(ex.Message);
        //     }
        // }
    }
}
