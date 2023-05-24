using Ms_Compras.Context;
using Ms_Compras.Database.Entidades;
using Ms_Compras.Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using Ms_Compras.Dtos.CompraFornecedor;

namespace Ms_Compras.Database.Repository
{
    public class CompraFornecedorRepository : GenericRepository<CompraFornecedor>, ICompraFornecedorRepository
    {
        public async Task<List<CompraFornecedor>> ConsultarPedido(FiltroPedidoCompraFornecedor filtroPedido, Guid empresaId)
        {
            try
            {
                var comprasFornecedor = new List<CompraFornecedor>();

                using (var context = new MsContext(this._OptionsBuilder))
                {
                    IQueryable<CompraFornecedor> todasCompras = context.CompraFornecedor
                        .Where(x => x.DataDeCadastro >= filtroPedido.DataInicial &&
                                    x.DataDeCadastro <= filtroPedido.DataFinal)
                        .Include(x => x.ItensCompraFornecedor)
                            .ThenInclude(item => item.Produto)
                                .ThenInclude(x => x.Grupo)
                        .Include(x => x.Fornecedor);

                    todasCompras = filtroPedido.StatusPedido is not null && filtroPedido.StatusPedido > 0 ?
                        todasCompras.Where(x => x.StatusPedido == filtroPedido.StatusPedido) : todasCompras;

                    todasCompras = filtroPedido.FornecedorId is not null ?
                        todasCompras.Where(x => x.FornecedorId == filtroPedido.FornecedorId) : todasCompras;

                    todasCompras = filtroPedido.GrupoId is not null ?
                        todasCompras.Where(x => x.ItensCompraFornecedor.Any(y => y.Produto.GrupoId == filtroPedido.GrupoId)) : todasCompras;

                    todasCompras = filtroPedido.ProdutoId is not null ?
                        todasCompras.Where(x => x.ItensCompraFornecedor.Any(y => y.ProdutoId == filtroPedido.ProdutoId)) : todasCompras;

                    todasCompras = filtroPedido.CompraId is not null ?
                        todasCompras.Where(x => x.CompraId == filtroPedido.CompraId) : todasCompras;

                    todasCompras = filtroPedido.CompraFornecedorId is not null ?
                        todasCompras.Where(x => x.Id == filtroPedido.CompraFornecedorId) : todasCompras;

                    comprasFornecedor = await todasCompras.OrderByDescending(x => x.DataDeCadastro).Include(x => x.ItensCompraFornecedor).ToListAsync();

                    return comprasFornecedor;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CompraFornecedor> GetByIdAsync(Guid id, Guid empresaId)
        {
			try
			{
				using (var context = new MsContext(_OptionsBuilder))
				{
					return await context.CompraFornecedor
						.Include(x => x.ItensCompraFornecedor)
							.ThenInclude(x => x.Produto)
								.ThenInclude(x => x.Grupo)
						.Include(x => x.Fornecedor)
						.FirstOrDefaultAsync(x => x.Id == id && !x.Excluido && x.EmpresaId == empresaId);
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
        }

        public async Task<List<CompraFornecedor>> GetFiltroCotacaoCompras(FiltroCotacaoCompraDto filtroCotacaoCompraDto, Guid empresaId)
        {
            try
            {
                using (var data = new MsContext(_OptionsBuilder))
                {
                    return await data.CompraFornecedor
                        .Where(x => x.CompraId == filtroCotacaoCompraDto.CompraId
                            && x.DataDeCadastro >= filtroCotacaoCompraDto.DataInicial
                            && x.DataDeCadastro <= filtroCotacaoCompraDto.DataFinal
                            && x.EmpresaId == empresaId
                            && !x.Excluido)
                        .Include(x => x.ItensCompraFornecedor)
                            .ThenInclude(x => x.Produto)
                                .ThenInclude(x => x.Grupo)
                        .Include(x => x.Fornecedor)
                        .ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<CompraFornecedor>> GetListByCompraIdAsync(Guid id, Guid empresaId)
        {
			try
			{
				using (var context = new MsContext(_OptionsBuilder))
				{
					return await context.CompraFornecedor
						.Include(x => x.ItensCompraFornecedor)
							.ThenInclude(x => x.Produto)
								.ThenInclude(x => x.Grupo)
						.Include(x => x.Fornecedor)
						.Where(x => x.CompraId == id && x.EmpresaId == empresaId && !x.Excluido)
						.ToListAsync();
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
        }
    }
}
