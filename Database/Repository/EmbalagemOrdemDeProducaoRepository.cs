using Ms_Compras.Database.Entidades;
using Ms_Compras.Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using Ms_Compras.Context;
using Ms_Compras.Dtos.FiltrosCompra;

namespace Ms_Compras.Database.Repository
{
    public class EmbalagemOrdemDeProducaoRepository : GenericRepository<EmbalagemOrdemDeProducao>, IEmbalagemOrdemDeProducaoRepository
    {
        public async Task<List<EmbalagemOrdemDeProducao>> GetEmbalagemOrdemDeProducaoFiltroNoCabecalho(FiltroCompraConsumo filtroCompraDto, Guid numeroOrdemDeProducao)
        {
            try
            {
                if (filtroCompraDto.ProdutoId is null)
                {
                    using (var data = new MsContext(_OptionsBuilder))
                    {
                        return await data.EmbalagemOrdemDeProducao
                            .AsNoTracking()
                            .Include(x => x.Produto)
                            .Include(x => x.Grupo)
                            .Where(x => !x.Excluido &&
                                    x.EmpresaId == filtroCompraDto.EmpresaId &&
                                    x.OrdemDeProducaoId == numeroOrdemDeProducao &&
                                    filtroCompraDto.GrupoId.Contains(x.GrupoId))
                                .ToListAsync();
                    }
                }
                else
                {
                    using (var data = new MsContext(_OptionsBuilder))
                    {
                        return await data.EmbalagemOrdemDeProducao
                            .AsNoTracking()
                            .Include(x => x.Produto)
                            .Include(x => x.Grupo)
                            .Where(x => !x.Excluido &&
                                    x.EmpresaId == filtroCompraDto.EmpresaId &&
                                    x.OrdemDeProducaoId == numeroOrdemDeProducao &&
                                    filtroCompraDto.GrupoId.Contains(x.GrupoId) &&
                                    filtroCompraDto.ProdutoId.Contains(x.ProdutoId) &&
                                    !x.Produto.InativoCompras)
                                .ToListAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<EmbalagemOrdemDeProducao>> GetEmbalagemOrdemDeProducaoFiltroNosItens(FiltroCompraConsumo filtroCompraDto, Guid numeroOrdemDeProducao)
        {
            try
            {
                if (filtroCompraDto.ProdutoId is null)
                {
                    using (var data = new MsContext(_OptionsBuilder))
                    {
                        return await data.EmbalagemOrdemDeProducao
                            .AsNoTracking()
                            .Include(x => x.ItensEmbalagemOrdemDeProducao)
                                .ThenInclude(x => x.Produto)
                                    .ThenInclude(x => x.Grupo)
                            .Where(x => !x.Excluido &&
                                x.EmpresaId == filtroCompraDto.EmpresaId &&
                                x.ItensEmbalagemOrdemDeProducao.Any(x => filtroCompraDto.GrupoId.Contains(x.Produto.GrupoId)))
                            .ToListAsync();
                    }
                }
                else
                {
                    using (var data = new MsContext(_OptionsBuilder))
                    {
                        return await data.EmbalagemOrdemDeProducao
                            .AsNoTracking()
                            .Include(x => x.ItensEmbalagemOrdemDeProducao)
                                .ThenInclude(x => x.Produto)
                                    .ThenInclude(x => x.Grupo)
                            .Where(x => !x.Excluido &&
                                x.EmpresaId == filtroCompraDto.EmpresaId &&
                                x.ItensEmbalagemOrdemDeProducao.Any(x =>
                                    filtroCompraDto.GrupoId.Contains(x.Produto.GrupoId) && 
                                    filtroCompraDto.ProdutoId.Contains(x.ProdutoId) &&
                                    !x.Produto.Inativo))
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