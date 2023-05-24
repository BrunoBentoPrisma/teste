using Microsoft.EntityFrameworkCore;
using Ms_Compras.Context;
using Ms_Compras.Database.Entidades;
using Ms_Compras.Database.Interfaces;
using Ms_Compras.Dtos.FiltrosCompra;

namespace Ms_Compras.Database.Repository
{
    public class OrdemDeProducaoRepository : GenericRepository<OrdemDeProducao>, IOrdemDeProducaoRepository
    {
        public async Task<List<OrdemDeProducao>> GetOrdemDeProducaoFiltroNosItens(FiltroCompraConsumo filtroCompraDto)
        {
            try
            {
                // grupos do tipo (0,1,6,7)
                using (var data = new MsContext(_OptionsBuilder))
                {
                    if (filtroCompraDto.ProdutoId is not null && filtroCompraDto.ProdutoId.Count() > 0)
                    {
                        return await data.OrdemDeProducao
                            .AsNoTracking()
                            .Include(x => x.ItensOrdemDeProducao)
                                .ThenInclude(x => x.Produto)
                                    .ThenInclude(x => x.Laboratorio)
                            .Where(x => !x.Excluido &&
                                    x.EmpresaId == filtroCompraDto.EmpresaId &&
                                    x.DataDeEmissao <= filtroCompraDto.DataFinal &&
                                    x.DataDeEmissao >= filtroCompraDto.DataInicial &&
                                    x.Status == 2 &&
                                    x.ItensOrdemDeProducao.Any(x => filtroCompraDto.ProdutoId.Contains(x.ProdutoId) &&
                                        !x.Produto.InativoCompras &&
                                        !x.Produto.Inativo &&
                                        !x.Excluido))
                            .ToListAsync();
                    }
                    else
                    {
                        return await data.OrdemDeProducao
                            .AsNoTracking()
                            .Include(x => x.ItensOrdemDeProducao)
                                .ThenInclude(x => x.Produto)
                                    .ThenInclude(x => x.Laboratorio)
                            .Where(x => !x.Excluido &&
                                    x.EmpresaId == filtroCompraDto.EmpresaId &&
                                    x.ItensOrdemDeProducao.Any(x => filtroCompraDto.GrupoId.Contains(x.Produto.GrupoId)) &&
                                    x.DataDeEmissao <= filtroCompraDto.DataFinal &&
                                    x.DataDeEmissao >= filtroCompraDto.DataInicial &&
                                    x.Status == 2)
                            .ToListAsync();
                    }

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<OrdemDeProducao>> GetOrdemDeProducaoFiltroNoCabecalho(FiltroCompraConsumo filtroCompraDto)
        {
            try
            {
                if (filtroCompraDto.ProdutoId is null)
                {
                    using (var data = new MsContext(_OptionsBuilder))
                    {
                        return await data.OrdemDeProducao
                            .AsNoTracking()
                            .Where(x => !x.Excluido &&
                                    x.EmpresaId == filtroCompraDto.EmpresaId &&
                                    x.DataDeEmissao <= filtroCompraDto.DataFinal &&
                                    x.DataDeEmissao >= filtroCompraDto.DataInicial &&
                                    x.Status == 2)
                                .ToListAsync();
                    }
                }
                else
                {
                    using (var data = new MsContext(_OptionsBuilder))
                    {
                        return await data.OrdemDeProducao
                                .AsNoTracking()
                                .Where(x => !x.Excluido &&
                                        x.EmpresaId == filtroCompraDto.EmpresaId &&
                                        x.DataDeEmissao <= filtroCompraDto.DataFinal &&
                                        x.DataDeEmissao >= filtroCompraDto.DataInicial &&
                                        x.Status == 2)
                                    .ToListAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}