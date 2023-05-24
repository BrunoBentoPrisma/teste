using Ms_Compras.Database.Entidades;
using Ms_Compras.Database.Interfaces;
using Ms_Compras.Dtos;
using Ms_Compras.Dtos.Compra;
using Ms_Compras.Dtos.FiltrosCompra;
using Ms_Compras.Services.Interfaces;

namespace Ms_Compras.Services.Service
{
    public class FiltroCompraTipoVendaService : IFiltroCompraTipoVendaService
    {
        private readonly IVendaRepository _vendaRepository;
        private readonly ILoteRepository _loteRepository;
        private readonly IConfigEstoqueFilialRepository _configEstoqueFilialRepository;
        private readonly INotaFiscalDeEntradaRepository _notaFiscalDeEntradaRepository;
        private readonly IFiltroCompraTipoFaltasEncomendasService _filtroCompraTipoFaltasEncomendasService;
        private readonly IMovimentoProdutoRepository _movimentoProdutoRepository;

        public FiltroCompraTipoVendaService(IVendaRepository vendaRepository,
            ILoteRepository loteRepository,
            IConfigEstoqueFilialRepository configEstoqueFilialRepository,
            INotaFiscalDeEntradaRepository notaFiscalDeEntradaRepository,
            IFiltroCompraTipoFaltasEncomendasService faltasEncomendasService,
            IMovimentoProdutoRepository movimentoProdutoRepository)
        {
            _movimentoProdutoRepository = movimentoProdutoRepository;
            _filtroCompraTipoFaltasEncomendasService = faltasEncomendasService;
            _notaFiscalDeEntradaRepository = notaFiscalDeEntradaRepository;
            _loteRepository = loteRepository;
            _configEstoqueFilialRepository = configEstoqueFilialRepository;
            _vendaRepository = vendaRepository;
        }

        public async Task<List<ItensCompraFiltroViewDto>> GetFiltroItensCompra(FiltroCompraDto filtroCompraDto)
        {
            try
            {
                var filtroCompraPorVenda = new FiltroCompraVenda(filtroCompraDto.VendaDe,
                    filtroCompraDto.VendaAte,
                    filtroCompraDto.EmpresaId,
                    filtroCompraDto.APartirDe,
                    filtroCompraDto.LaboratorioId,
                    filtroCompraDto.GruposIds,
                    filtroCompraDto.ProdutosIds,
                    filtroCompraDto.CurvaAbc);

                var itensVendasFiltro = await _vendaRepository.GetFiltroCompraTipoVenda(filtroCompraPorVenda);

                if (itensVendasFiltro is null) throw new Exception("Falha ao filtrar os itens das compras !");

                var itensCompraViewDto = new List<ItensCompraFiltroViewDto>();

                var configDeEstoqueMinimo = await _configEstoqueFilialRepository.GetControlaEstoqueMinimoMaximoPorFilial(filtroCompraDto.EmpresaId.Value);

                var itensVendasGroup = itensVendasFiltro.GroupBy(x => x.ProdutoId);

                foreach (var itensVendas in itensVendasGroup)
                {
                    var contador = 0;

                    var itemCompraViewDto = new ItensCompraFiltroViewDto();
                    
                    foreach (var item in itensVendas)
                    {

                        decimal estoqueMinimo = 0;
                        decimal estoqueMaximo = 0;

                        itemCompraViewDto.QuantidadeVendida += item.Quantidade;
                        
                        contador++;

                        if (contador == itensVendas.Count())
                        {
                            itemCompraViewDto.CodigoCas = item.Produto.CodigoCas;
                            itemCompraViewDto.CodigoDcb = item.Produto.CodigoDcb;
                            itemCompraViewDto.CodigoBarras = item.Produto.CodigoBarra;
                            itemCompraViewDto.ValorVendido = item.Produto.ValorVenda is not null ? item.Produto.ValorVenda.Value * itemCompraViewDto.QuantidadeVendida : 0;
                            itemCompraViewDto.GrupoId = item.Produto.GrupoId;
                            itemCompraViewDto.ProdutoId = item.ProdutoId;
                            itemCompraViewDto.DescricaoProduto = item.Produto.Descricao;
                            itemCompraViewDto.CurvaAbc = item.Produto.CurvaAbc == 0 ? "A" : item.Produto.CurvaAbc == 1 ? "B" : item.Produto.CurvaAbc == 2 ? "C" : null;
                            itemCompraViewDto.ValorUnitario = item.ValorUnitario;

                            if (item.Produto.Laboratorio is not null)
                                itemCompraViewDto.DescricaoLaboratorio = item.Produto.Laboratorio.Descricao;

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

                            var quantidadeComprometidade = await _loteRepository.GetQuantidadeComprometida(item.ProdutoId, item.Produto.GrupoId, filtroCompraDto.EmpresaId.Value);

                            itemCompraViewDto.Estoque = await _movimentoProdutoRepository.GetQuantidadeSaldo(item.ProdutoId, filtroCompraDto.EmpresaId.Value)
                            + quantidadeComprometidade;

                            if (filtroCompraDto.SaldoQuantidadeComprometida is not null && filtroCompraDto.SaldoQuantidadeComprometida.Value)
                            {
                                itemCompraViewDto.Quantidade += quantidadeComprometidade;
                            }

                            var total = itemCompraViewDto.Quantidade - itemCompraViewDto.Estoque;

                            itemCompraViewDto.Quantidade = total > itemCompraViewDto.Estoque ? total : 0;
                            itemCompraViewDto.Comprar = itemCompraViewDto.Quantidade > 0;
                            itemCompraViewDto.ValorTotal = itemCompraViewDto.Comprar ? itemCompraViewDto.ValorUnitario * itemCompraViewDto.Quantidade : 0;

                            var nfEntrada = await _notaFiscalDeEntradaRepository.GetUltimaCompra(item.ProdutoId, filtroCompraDto.EmpresaId.Value);

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

                    }
                }

                if (filtroCompraDto.ConsideraEncomendaFaltas is not null && filtroCompraDto.ConsideraEncomendaFaltas.Value)
                {
                    var faltasEncomendasFiltro = await _filtroCompraTipoFaltasEncomendasService.GetFiltroItensCompra(filtroCompraDto);

                    faltasEncomendasFiltro.ForEach(x =>
                    {
                        var itemFiltro = itensCompraViewDto.FirstOrDefault(y => y.ProdutoId == x.ProdutoId);

                        if(itemFiltro is null)
                        {
                            itensCompraViewDto.Add(x);
                        }
                        else
                        {
                            itemFiltro.Quantidade += x.Quantidade;
                            itemFiltro.ValorTotal = itemFiltro.ValorVendido * x.Quantidade;
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
