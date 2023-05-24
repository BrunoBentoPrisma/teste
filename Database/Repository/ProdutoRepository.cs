using Microsoft.EntityFrameworkCore;
using Ms_Compras.Context;
using Ms_Compras.Database.Entidades;
using Ms_Compras.Database.Interfaces;
using Ms_Compras.Dtos.FiltrosCompra;

namespace Ms_Compras.Database.Repository
{
    public class ProdutoRepository : GenericRepository<Produto>, IProdutoRepository
    {
        public async Task<List<Produto>> GetFiltroCompraEstoqueMinimoMaximo(FiltroCompraEstoqueMinimoEstoqueMaximo filtro)
        {
            try
            {
                using (var data = new MsContext(_OptionsBuilder))
                {

                    var produtos = await data.Produto
                        .AsNoTracking()
                        .Include(x => x.Grupo)
                        .Include(x => x.Laboratorio)
                        .Include(x => x.Fornecedor)
                        .Where(x => x.EmpresaId == filtro.EmpresaId && !x.Excluido && !x.Inativo && !x.InativoCompras)
                        .ToListAsync();

                    produtos = filtro.LaboratorioId is not null ? produtos.Where(x => x.LaboratorioId == filtro.LaboratorioId).ToList() : produtos;

                    produtos = filtro.GruposIds is not null && filtro.GruposIds.Count > 0 ? produtos.Where(x => filtro.GruposIds.Contains(x.GrupoId)).ToList() : produtos;

                    produtos = filtro.ProdutosIds is not null && filtro.ProdutosIds.Count > 0 ? produtos.Where(x => filtro.ProdutosIds.Contains(x.Id)).ToList() : produtos;

                    return produtos;

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Produto> GetProdutoByIdAsync(Guid id, Guid empresaId)
        {
            using (var data = new MsContext(_OptionsBuilder))
            {
                return await data.Produto
                    .AsNoTracking()
                    .Include(x => x.Grupo)
                    .SingleOrDefaultAsync(x => x.EmpresaId == empresaId && !x.Excluido && x.Id == id);
            }
        }

        public async Task<Produto> GetProdutoByIntegracaoId(string integracaoId)
        {
            using (var data = new MsContext(_OptionsBuilder))
            {
                return await data.Produto
                    .AsNoTracking()
                    .Include(x => x.Grupo)
                    .SingleOrDefaultAsync(x => x.IntegracaoId == integracaoId && !x.Excluido);
            }
        }
    }
}
