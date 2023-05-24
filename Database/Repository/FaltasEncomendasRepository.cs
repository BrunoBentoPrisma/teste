using Microsoft.EntityFrameworkCore;
using Ms_Compras.Context;
using Ms_Compras.Database.Entidades;
using Ms_Compras.Database.Interfaces;
using Ms_Compras.Dtos.FiltrosCompra;
using Ms_Compras.Enum;

namespace Ms_Compras.Database.Repository
{
    public class FaltasEncomendasRepository : GenericRepository<FaltasEncomendas>, IFaltasEncomendasRepository
    {
        public async Task<List<FaltasEncomendas>> GetAllAsync(Guid empresaId)
        {
            try
            {
                using (var data = new MsContext(_OptionsBuilder))
                {
                    return await data.FaltasEncomendas
                        .Include(x => x.Produto)
                        .Include(x => x.Grupo)
                        .Include(x => x.Cliente)
                        .Include(x => x.Vendedor)
                        .Where(x => x.EmpresaId == empresaId && !x.Excluido)
                        .ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<FaltasEncomendas> GetByIdAsync(Guid id, Guid empresaId)
        {
            try
            {
                using (var data = new MsContext(_OptionsBuilder))
                {
                    return await data.FaltasEncomendas
                        .Include(x => x.Produto)
                        .Include(x => x.Grupo)
                        .Include(x => x.Cliente)
                        .Include(x => x.Vendedor)
                        .FirstOrDefaultAsync(x => x.Id == id && x.EmpresaId == empresaId && !x.Excluido);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<FaltasEncomendas>> GetFiltroCompraPorPeriodoAsync(FiltroCompraFaltasEncomendas filtroCompraFaltasEncomendasDto)
        {
            try
            {
                using (var data = new MsContext(_OptionsBuilder))
                {


                    var faltasEncomendas = await data.FaltasEncomendas
                            .AsNoTracking()
                            .Include(x => x.Produto)
                                .ThenInclude(x => x.Laboratorio)
                            .Where(x =>
                                x.EmpresaId == filtroCompraFaltasEncomendasDto.EmpresaId &&
                                !x.Excluido &&
                                !x.Produto.Inativo &&
                                !x.Produto.InativoCompras &&
                                x.Status == 0
                            ).ToListAsync();

                    faltasEncomendas = filtroCompraFaltasEncomendasDto.LaboratorioId is not null ?
                        faltasEncomendas.Where(x => x.Produto.LaboratorioId == filtroCompraFaltasEncomendasDto.LaboratorioId).ToList() : faltasEncomendas;

                    if (filtroCompraFaltasEncomendasDto.CurvaAbc is not null)
                    {
                        if (filtroCompraFaltasEncomendasDto.CurvaAbc != CurvaAbc.Geral)
                        {
                            faltasEncomendas = filtroCompraFaltasEncomendasDto.CurvaAbc == CurvaAbc.A ? faltasEncomendas.Where(x => x.Produto.CurvaAbc == 0).ToList() :
                                filtroCompraFaltasEncomendasDto.CurvaAbc == CurvaAbc.B ? faltasEncomendas.Where(x => x.Produto.CurvaAbc == 1).ToList() :
                                faltasEncomendas.Where(x => x.Produto.CurvaAbc == 2).ToList();
                        }
                    }

                    return faltasEncomendas;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<decimal> GetQuantidadeFaltaEncomenda(Guid produtoId, Guid empresaId)
        {
            try
            {
                using (var data = new MsContext(_OptionsBuilder))
                {
                    return await data.FaltasEncomendas
                        .Where(x => x.EmpresaId == empresaId &&
                            !x.Excluido &&
                            x.ProdutoId == produtoId &&
                            x.Status == 0
                            )
                        .SumAsync(x => x.Quantidade);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
