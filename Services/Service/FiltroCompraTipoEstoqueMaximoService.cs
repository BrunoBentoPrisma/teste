using Ms_Compras.Database.Interfaces;
using Ms_Compras.Dtos;
using Ms_Compras.Dtos.Compra;
using Ms_Compras.Dtos.FiltrosCompra;
using Ms_Compras.Services.Interfaces;

namespace Ms_Compras.Services.Service
{
    public class FiltroCompraTipoEstoqueMaximoService : IFiltroCompraTipoEstoqueMaximoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ILoteRepository _loteRepository;
        private readonly IConfigEstoqueFilialRepository _configEstoqueFilialRepository;
        private readonly INotaFiscalDeEntradaRepository _notaFiscalDeEntradaRepository;
        private readonly IFiltroCompraTipoFaltasEncomendasService _filtroCompraTipoFaltasEncomendasService;
        private readonly IMovimentoProdutoRepository _movimentoProdutoRepository;

        public FiltroCompraTipoEstoqueMaximoService(IProdutoRepository produtoRepository, 
            ILoteRepository loteRepository, 
            IConfigEstoqueFilialRepository configEstoqueFilialRepository, 
            INotaFiscalDeEntradaRepository notaFiscalDeEntradaRepository,
            IFiltroCompraTipoFaltasEncomendasService faltasEncomendasService, 
            IMovimentoProdutoRepository movimentoProdutoRepository)
        {
            _produtoRepository = produtoRepository;
            _loteRepository = loteRepository;
            _configEstoqueFilialRepository = configEstoqueFilialRepository;
            _notaFiscalDeEntradaRepository = notaFiscalDeEntradaRepository;
            _filtroCompraTipoFaltasEncomendasService = faltasEncomendasService;
            _movimentoProdutoRepository = movimentoProdutoRepository;
        }

        public async Task<List<ItensCompraFiltroViewDto>> GetFiltroItensCompra(FiltroCompraDto filtroCompraDto)
        {
            try
            {
                var itensCompraViewDto = new List<ItensCompraFiltroViewDto>();

                var filtroCompraEstoqueMinimoEstoqueMaximo = new FiltroCompraEstoqueMinimoEstoqueMaximo(filtroCompraDto.EmpresaId.Value,
                                                                    filtroCompraDto.LaboratorioId, filtroCompraDto.GruposIds,filtroCompraDto.ProdutosIds);

                var produtos = await _produtoRepository.GetFiltroCompraEstoqueMinimoMaximo(filtroCompraEstoqueMinimoEstoqueMaximo);
                
                var configDeEstoqueMinimo = await _configEstoqueFilialRepository.GetControlaEstoqueMinimoMaximoPorFilial(filtroCompraDto.EmpresaId.Value);

                foreach(var produto in produtos)
                {
                    var itemCompraViewDto = new ItensCompraFiltroViewDto();

                    decimal estoqueMinimo = 0;
                    decimal estoqueMaximo = 0;

                    itemCompraViewDto.CodigoCas = produto.CodigoCas;
                    itemCompraViewDto.CodigoDcb = produto.CodigoDcb;
                    itemCompraViewDto.CodigoBarras = produto.CodigoBarra;
                    itemCompraViewDto.ValorVendido = 0;
                    itemCompraViewDto.GrupoId = produto.GrupoId;
                    itemCompraViewDto.ProdutoId = produto.Id;
                    itemCompraViewDto.DescricaoProduto = produto.Descricao;
                    itemCompraViewDto.CurvaAbc = produto.CurvaAbc == 0 ? "A" : produto.CurvaAbc == 1 ? "B" : produto.CurvaAbc == 2 ? "C" : null;
                    itemCompraViewDto.ValorUnitario = produto.ValorCusto;

                    if (produto.Laboratorio is not null)
                        itemCompraViewDto.DescricaoLaboratorio = produto.Laboratorio.Descricao;

                    if (configDeEstoqueMinimo)
                    {
                        var configEstoque = await _configEstoqueFilialRepository.GetConfigEstoqueFilialAsync(filtroCompraDto.EmpresaId.Value, produto.Id);

                        estoqueMinimo = configEstoque.EstoqueMinimo;
                        estoqueMaximo = configEstoque.EstoqueMaximo;

                    }
                    else
                    {
                        estoqueMinimo = produto.EstoqueMinimo is not null ? produto.EstoqueMinimo.Value : 0;
                        estoqueMaximo = produto.EstoqueMaximo is not null ? produto.EstoqueMaximo.Value : 0;
                    }

                    itemCompraViewDto.EstoqueMinimo = estoqueMinimo;
                    itemCompraViewDto.EstoqueMaximo = estoqueMaximo;

                    var quantidadeComprometida = await _loteRepository.GetQuantidadeComprometida(produto.Id, produto.GrupoId, filtroCompraDto.EmpresaId.Value);

                    itemCompraViewDto.Estoque = await _movimentoProdutoRepository.GetQuantidadeSaldo(produto.Id, filtroCompraDto.EmpresaId.Value)
                            + quantidadeComprometida;

                    if(itemCompraViewDto.EstoqueMaximo >= itemCompraViewDto.Estoque)
                    {
                        itemCompraViewDto.Quantidade = itemCompraViewDto.EstoqueMaximo - itemCompraViewDto.Estoque;
                        itemCompraViewDto.ValorTotal = itemCompraViewDto.ValorUnitario * itemCompraViewDto.Quantidade;
                    }


                    if (filtroCompraDto.SaldoQuantidadeComprometida is not null && filtroCompraDto.SaldoQuantidadeComprometida.Value)
                    {
                        itemCompraViewDto.Quantidade += quantidadeComprometida;

                        itemCompraViewDto.ValorTotal = itemCompraViewDto.ValorUnitario * itemCompraViewDto.Quantidade;
                    }

                    itemCompraViewDto.Comprar = itemCompraViewDto.Quantidade > 0;

                    var nfEntrada = await _notaFiscalDeEntradaRepository.GetUltimaCompra(produto.Id, filtroCompraDto.EmpresaId.Value);

                    if (nfEntrada is not null)
                    {
                        if (nfEntrada.Fornecedor is not null)
                        {
                            itemCompraViewDto.NomeFornecedor = nfEntrada.Fornecedor.NomeFornecedor;
                            itemCompraViewDto.Fornecedores.Add(nfEntrada.Fornecedor);
                        }
                    }

                    itensCompraViewDto.Add(itemCompraViewDto);

                }

                if (filtroCompraDto.ConsideraEncomendaFaltas is not null && filtroCompraDto.ConsideraEncomendaFaltas.Value)
                {
                    var faltasEncomendasFiltro = await _filtroCompraTipoFaltasEncomendasService.GetFiltroItensCompra(filtroCompraDto);

                    faltasEncomendasFiltro.ForEach(x =>
                    {
                        var itemFiltro = itensCompraViewDto.FirstOrDefault(y => y.ProdutoId == x.ProdutoId);

                        if (itemFiltro is null)
                        {
                            itensCompraViewDto.Add(x);
                        }
                        else
                        {
                            itemFiltro.Quantidade += x.Quantidade;
                            itemFiltro.ValorTotal = itemFiltro.ValorUnitario * itemFiltro.Quantidade;
                            itensCompraViewDto.Remove(itemFiltro);
                            itensCompraViewDto.Add(itemFiltro);
                        }

                    });
                }

                return itensCompraViewDto.OrderBy(x => x.DescricaoProduto).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
