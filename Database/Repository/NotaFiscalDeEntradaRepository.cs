using Microsoft.EntityFrameworkCore;
using Ms_Compras.Context;
using Ms_Compras.Database.Entidades;
using Ms_Compras.Database.Interfaces;
using Ms_Compras.Dtos.NotaFiscalDeEntrada;

namespace Ms_Compras.Database.Repository
{
    public class NotaFiscalDeEntradaRepository : GenericRepository<NotaFiscalDeEntrada>, INotaFiscalDeEntradaRepository
    {
        public async Task<NotaFiscalDeEntrada> GetUltimaCompra(Guid produtoId, Guid empresaId)
        {
            try
            {
                using(var data = new MsContext(_OptionsBuilder))
                {
                    return await data.NotaFiscalDeEntrada
                        .AsNoTracking()
                        .Include(x => x.ItensNotaFiscalDeEntrada)
                            .ThenInclude(x => x.Produto)
                                .ThenInclude(x => x.Laboratorio)
                        .Include(x => x.Fornecedor)
                        .OrderByDescending(x => x.DataDeEmissao)
                        .FirstOrDefaultAsync(x => x.ItensNotaFiscalDeEntrada.Any(x => x.ProdutoId == produtoId) && x.EmpresaId == empresaId && !x.Excluido);
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<NotaFiscalDeEntrada>> GetUltimasComprasPorPeriodo(FiltroUltimasCompras filtroUltimasCompras, Guid empresaId)
        {
            try
            {
                using (var data = new MsContext(_OptionsBuilder))
                {
                    return await data.NotaFiscalDeEntrada
                        .AsNoTracking()
                        .Include(x => x.ItensNotaFiscalDeEntrada)
                            .ThenInclude(x => x.Produto)
                                .ThenInclude(x => x.Grupo)
                        .Include(x => x.Fornecedor)
                        .Where(x => !x.Excluido && x.EmpresaId == empresaId &&
                                x.DataDeEmissao >= filtroUltimasCompras.DataInicial &&
                                x.DataDeEmissao <= filtroUltimasCompras.DataFinal &&
                                x.ItensNotaFiscalDeEntrada.Any(
                                               x => x.ProdutoId == filtroUltimasCompras.ProdutoId &&
                                               x.Produto.GrupoId == filtroUltimasCompras.GrupoId)
                            ).ToListAsync();

                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}