using Microsoft.EntityFrameworkCore;
using Ms_Compras.Context;
using Ms_Compras.Database.Entidades;
using Ms_Compras.Database.Interfaces;
using Ms_Compras.Dtos.FiltrosCompra;

namespace Ms_Compras.Database.Repository
{
    public class VendaRepository : GenericRepository<Venda>, IVendaRepository
    {
        public async Task<List<Venda>> GetFiltroCompra(FiltroCompraConsumo filtroCompraDto)
        {
            try
            {
                if (filtroCompraDto.ProdutoId is null)
                {
                    using (var data = new MsContext(_OptionsBuilder))
                    {
                        return await data.Venda
                            .AsNoTracking()
                            .Include(x => x.ItensVenda)
                                .ThenInclude(x => x.Produto)
                                    .ThenInclude(x => x.Grupo)
                            .Where(x => !x.Excluido &&
                                x.EmpresaId == filtroCompraDto.EmpresaId &&
                                x.Orcamento == 0 &&
                                x.Status == 1 &&
                                x.DataDeEmissao <= filtroCompraDto.DataFinal &&
                                x.DataDeEmissao >= filtroCompraDto.DataInicial &&
                                x.ItensVenda.Any(x => filtroCompraDto.GrupoId.Contains(x.Produto.GrupoId)))
                            .ToListAsync();
                    }
                }
                else
                {
                    using (var data = new MsContext(_OptionsBuilder))
                    {
                        return await data.Venda
                            .AsNoTracking()
                            .Include(x => x.ItensVenda)
                                .ThenInclude(x => x.Produto)
                                    .ThenInclude(x => x.Grupo)
                            .Where(x => !x.Excluido &&
                                x.EmpresaId == filtroCompraDto.EmpresaId &&
                                x.Orcamento == 0 &&
                                x.Status == 1 &&
                                x.DataDeEmissao <= filtroCompraDto.DataFinal &&
                                x.DataDeEmissao >= filtroCompraDto.DataInicial &&
                                x.ItensVenda.Any(x => filtroCompraDto.GrupoId.Contains(x.Produto.GrupoId) && 
                                filtroCompraDto.ProdutoId.Contains(x.ProdutoId)))
                            .ToListAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ItensVenda>> GetFiltroCompraTipoVenda(FiltroCompraVenda filtroCompraPorVenda)
        {
            try
            {
                using (var data = new MsContext(_OptionsBuilder))
                {
                    var vendas = await data.Venda
                        .AsNoTracking()
                        .Include(x => x.ItensVenda)
                            .ThenInclude(x => x.Produto)
                                .ThenInclude(x => x.Laboratorio)
                        .Where(x => !x.Excluido && x.EmpresaId == filtroCompraPorVenda.EmpresaId &&
                            x.Status == 1 &&
                            x.Orcamento == 0 &&
                            x.DataDeEmissao >= filtroCompraPorVenda.DataInicial &&
                            x.DataDeEmissao <= filtroCompraPorVenda.DataFinal &&
                            x.ItensVenda.Any(x => !x.Produto.Inativo && !x.Produto.InativoCompras)
                        ).ToListAsync();

                    if (filtroCompraPorVenda.APartirDe is not null)
                        vendas = vendas.Where(x => x.DataDeEmissao >= filtroCompraPorVenda.APartirDe).ToList();

                    if (filtroCompraPorVenda.LaboratorioId is not null)
                        vendas = vendas.Where(x => x.ItensVenda.Any(x => x.Produto.LaboratorioId == filtroCompraPorVenda.LaboratorioId)).ToList();

                    if (filtroCompraPorVenda.GruposIds is not null && filtroCompraPorVenda.GruposIds.Count > 0)
                    {
                        var vendasAux = new List<Venda>();

                        foreach (var grupoId in filtroCompraPorVenda.GruposIds)
                        {
                            var aux = vendas.Where(x => x.ItensVenda.Any(x => x.Produto.GrupoId == grupoId)).ToList();

                            foreach (var venda in aux)
                            {
                                vendasAux.Add(venda);
                            }
                        }

                        vendas = vendasAux;
                    }

                    if (filtroCompraPorVenda.ProdutosIds is not null && filtroCompraPorVenda.ProdutosIds.Count > 0)
                    {
                        var vendasAux = new List<Venda>();

                        foreach (var produtoId in filtroCompraPorVenda.ProdutosIds)
                        {
                            var aux = vendas.Where(x => x.ItensVenda.Any(x => x.ProdutoId == produtoId)).ToList();

                            foreach (var venda in aux)
                            {
                                vendasAux.Add(venda);
                            }
                        }

                        vendas = vendasAux;
                    }

                    if (filtroCompraPorVenda.CurvaAbc is not null)
                    {
                        if (filtroCompraPorVenda.CurvaAbc == Enum.CurvaAbc.A)
                            vendas = vendas.Where(x => x.ItensVenda.Any(x => x.Produto.CurvaAbc == 0)).ToList();
                        if (filtroCompraPorVenda.CurvaAbc == Enum.CurvaAbc.B)
                            vendas = vendas.Where(x => x.ItensVenda.Any(x => x.Produto.CurvaAbc == 1)).ToList();
                        if (filtroCompraPorVenda.CurvaAbc == Enum.CurvaAbc.C)
                            vendas = vendas.Where(x => x.ItensVenda.Any(x => x.Produto.CurvaAbc == 3)).ToList();
                    }

                    return vendas.SelectMany(x => x.ItensVenda).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}