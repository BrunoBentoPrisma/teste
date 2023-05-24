using Ms_Compras.Dtos.Compra;
using Ms_Compras.Dtos.FiltrosCompra;
using Ms_Compras.Dtos;
using Ms_Compras.Services.Interfaces;
using Ms_Compras.Database.Interfaces;
using Ms_Compras.Database.Entidades;

namespace Ms_Compras.Services.Service
{
    public class FiltroCompraTipoFaltasEncomendasService : IFiltroCompraTipoFaltasEncomendasService
    {
        private readonly IFaltasEncomendasRepository _faltasEncomendasRepository;
        private readonly IConfigEstoqueFilialRepository _configEstoqueFilialRepository;
        private readonly INotaFiscalDeEntradaRepository _notaFiscalDeEntradaRepository;
        private readonly ILoteRepository _loteRepository;
        private readonly IMovimentoProdutoRepository _movimentoProdutoRepository;

        public FiltroCompraTipoFaltasEncomendasService(IFaltasEncomendasRepository faltasEncomendasRepository,
                IConfigEstoqueFilialRepository configEstoqueFilialRepository,
                ILoteRepository loteRepository,
                INotaFiscalDeEntradaRepository notaFiscalDeEntradaRepository,
                IMovimentoProdutoRepository movimentoProdutoRepository
            )
        {
            _movimentoProdutoRepository = movimentoProdutoRepository;
            _notaFiscalDeEntradaRepository = notaFiscalDeEntradaRepository;
            _loteRepository = loteRepository;
            _configEstoqueFilialRepository = configEstoqueFilialRepository;
            _faltasEncomendasRepository = faltasEncomendasRepository;
        }

        public async Task<List<ItensCompraFiltroViewDto>> GetFiltroItensCompra(FiltroCompraDto filtroCompraDto)
        {

            var filtro = new FiltroCompraFaltasEncomendas(filtroCompraDto.CurvaAbc,
                filtroCompraDto.TipoValor,
                filtroCompraDto.APartirDe,
                filtroCompraDto.LaboratorioId, filtroCompraDto.EmpresaId);

            var faltasEncomendas = await _faltasEncomendasRepository.GetFiltroCompraPorPeriodoAsync(filtro);

            var ItensCompraViewDto = new List<ItensCompraFiltroViewDto>();

            var configDeEstoqueMinimo = await _configEstoqueFilialRepository.GetControlaEstoqueMinimoMaximoPorFilial(filtroCompraDto.EmpresaId.Value);

            var faltasEncomendasGroupBy = faltasEncomendas.GroupBy(x => x.ProdutoId);

            foreach (var faltasEncomenda in faltasEncomendasGroupBy)
            {
                var contador = 0;

                var itemCompraViewDto = new ItensCompraFiltroViewDto();

                foreach (var item in faltasEncomenda)
                {

                    decimal estoqueMinimo = 0;
                    decimal estoqueMaximo = 0;

                    itemCompraViewDto.Quantidade += item.Quantidade;

                    contador++;

                    if (contador == faltasEncomenda.Count())
                    {
                        if (configDeEstoqueMinimo)
                        {
                            var configEstoque = await _configEstoqueFilialRepository.GetConfigEstoqueFilialAsync(filtroCompraDto.EmpresaId.Value, item.ProdutoId);

                            estoqueMinimo = configEstoque.EstoqueMinimo;
                            estoqueMaximo = configEstoque.EstoqueMaximo;

                        }
                        else
                        {
                            estoqueMinimo = item.Produto.EstoqueMinimo is not null ? item.Produto.EstoqueMinimo.Value : 0;
                            estoqueMaximo = item.Produto.EstoqueMaximo is not null ? item.Produto.EstoqueMaximo.Value : 0;
                        }

                        itemCompraViewDto.EstoqueMinimo = estoqueMinimo;
                        itemCompraViewDto.EstoqueMaximo = estoqueMaximo;

                        itemCompraViewDto.GrupoId = item.Produto.GrupoId;
                        itemCompraViewDto.ProdutoId = item.ProdutoId;
                        itemCompraViewDto.DescricaoProduto = item.Produto.Descricao;
                        itemCompraViewDto.ValorVendido = item.Produto.ValorVenda is not null ? item.Produto.ValorVenda.Value * itemCompraViewDto.QuantidadeVendida : 0;
                        itemCompraViewDto.ValorUnitario = item.Produto.ValorCusto;
                        itemCompraViewDto.ValorTotal = itemCompraViewDto.ValorUnitario * itemCompraViewDto.Quantidade;
                        itemCompraViewDto.Comprar = itemCompraViewDto.Quantidade > 0;
                        itemCompraViewDto.CodigoCas = item.Produto.CodigoCas;
                        itemCompraViewDto.CodigoDcb = item.Produto.CodigoDcb;
                        itemCompraViewDto.CodigoBarras = item.Produto.CodigoBarra;
                        itemCompraViewDto.CurvaAbc = item.Produto.CurvaAbc == 0 ? "A" : item.Produto.CurvaAbc == 1 ? "B" : item.Produto.CurvaAbc == 2 ? "C" : null;

                        if (item.Produto.Laboratorio is not null)
                            itemCompraViewDto.DescricaoLaboratorio = item.Produto.Laboratorio.Descricao;

                        var quantidadeComprometida = await _loteRepository.GetQuantidadeComprometida(item.ProdutoId, item.Produto.GrupoId, filtroCompraDto.EmpresaId.Value);

                        if (filtroCompraDto.SaldoQuantidadeComprometida is not null && filtroCompraDto.SaldoQuantidadeComprometida.Value)
                        {
                            itemCompraViewDto.Quantidade += quantidadeComprometida;
                        }

                        itemCompraViewDto.ValorTotal = itemCompraViewDto.ValorUnitario * itemCompraViewDto.Quantidade;

                        itemCompraViewDto.Estoque = await _movimentoProdutoRepository.GetQuantidadeSaldo(item.ProdutoId, filtroCompraDto.EmpresaId.Value)
                            + quantidadeComprometida;

                        var nfEntrada = await _notaFiscalDeEntradaRepository.GetUltimaCompra(item.ProdutoId, filtroCompraDto.EmpresaId.Value);

                        if (nfEntrada is not null)
                        {
                            if (nfEntrada.Fornecedor is not null)
                            {
                                itemCompraViewDto.NomeFornecedor = nfEntrada.Fornecedor.NomeFornecedor;
                                itemCompraViewDto.Fornecedores.Add(nfEntrada.Fornecedor);
                            }
                        }

                        ItensCompraViewDto.Add(itemCompraViewDto);
                    }

                }
            }

            return ItensCompraViewDto.OrderBy(x => x.DescricaoProduto).ToList();
        }
    }
}
