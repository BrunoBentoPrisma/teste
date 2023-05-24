using Ms_Compras.Database.Interfaces;
using Ms_Compras.Dtos;
using Ms_Compras.Dtos.Compra;
using Ms_Compras.Services.Interfaces;

namespace Ms_Compras.Services.Service
{
    public class FiltroCompraTipoDemandaService : IFiltroCompraTipoDemandaService
    {
        private readonly IFiltroComprasPorDemandaRepository _filtroComprasPorDemandaRepository;
        private readonly ILoteRepository _loteRepository;
        private readonly IConfigEstoqueFilialRepository _configEstoqueFilialRepository;
        private readonly INotaFiscalDeEntradaRepository _notaFiscalDeEntradaRepository;
        private readonly IFiltroCompraTipoFaltasEncomendasService _filtroCompraTipoFaltasEncomendasService;
        private readonly IMovimentoProdutoRepository _movimentoProdutoRepository;
        private readonly IProdutoRepository _produtoRepository;

        public FiltroCompraTipoDemandaService(IFiltroComprasPorDemandaRepository filtroComprasPorDemandaRepository, 
            ILoteRepository loteRepository, 
            IConfigEstoqueFilialRepository configEstoqueFilialRepository, 
            INotaFiscalDeEntradaRepository notaFiscalDeEntradaRepository, 
            IFiltroCompraTipoFaltasEncomendasService filtroCompraTipoFaltasEncomendasService, 
            IMovimentoProdutoRepository movimentoProdutoRepository,
            IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
            _filtroComprasPorDemandaRepository = filtroComprasPorDemandaRepository;
            _loteRepository = loteRepository;
            _configEstoqueFilialRepository = configEstoqueFilialRepository;
            _notaFiscalDeEntradaRepository = notaFiscalDeEntradaRepository;
            _filtroCompraTipoFaltasEncomendasService = filtroCompraTipoFaltasEncomendasService;
            _movimentoProdutoRepository = movimentoProdutoRepository;
        }

        public async Task<List<ItensCompraFiltroViewDto>> GetFiltroItensCompra(FiltroCompraDto filtroCompraDto)
        {
            try
            {
                if (filtroCompraDto is null || filtroCompraDto.EmpresaId is null || filtroCompraDto.VendaDe is null || filtroCompraDto.VendaAte is null || filtroCompraDto.TempoDeRep is null) 
                    throw new Exception("filtro de compra inválido para o tipo demanda");

                var itensCompraViewPorDemanda = await _filtroComprasPorDemandaRepository.GetListaFiltroDemanda(
                    filtroCompraDto.EmpresaId.Value,filtroCompraDto.VendaDe.Value,filtroCompraDto.VendaAte.Value);

                if (itensCompraViewPorDemanda is null) throw new Exception("Não foi possível finalizar os filtros");

                var itensCompraViewDto = new List<ItensCompraFiltroViewDto>();

                var configDeEstoqueMinimo = await _configEstoqueFilialRepository.GetControlaEstoqueMinimoMaximoPorFilial(filtroCompraDto.EmpresaId.Value);

                foreach(var itemCompra in itensCompraViewPorDemanda)
                {
                    var itemCompraViewDto = new ItensCompraFiltroViewDto();

                    decimal estoqueMinimo = 0;
                    decimal estoqueMaximo = 0;
                    decimal quantidade = 0;

                    var difData = filtroCompraDto.VendaAte.Value - filtroCompraDto.VendaDe.Value;
                    var difInteiro = Convert.ToDecimal(difData.TotalDays / 30);

                    if (difInteiro == 0) difInteiro = 1;

                    var consumoMedio = itemCompra.QuantidadeVendida / difInteiro;
                    decimal mes = 30;
                    decimal tempoDeRepo = decimal.Parse(filtroCompraDto.TempoDeRep.Value.ToString());
                    decimal tempoReposicao = tempoDeRepo / mes;

                    var produto = await _produtoRepository.GetProdutoByIdAsync(itemCompra.Id, filtroCompraDto.EmpresaId.Value);
                    var quantidadeComprometida = await _loteRepository.GetQuantidadeComprometida(itemCompra.Id,itemCompra.GrupoId, filtroCompraDto.EmpresaId.Value);
                    var saldoProduto = await _movimentoProdutoRepository.GetQuantidadeSaldo(itemCompra.Id, filtroCompraDto.EmpresaId.Value);

                    var estoque = saldoProduto + quantidadeComprometida;

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

                    if (filtroCompraDto.TipoDemanda == 0)
                    {
                        quantidade = (decimal)((consumoMedio * tempoReposicao) + estoqueMinimo - estoque);
                        itemCompraViewDto.Quantidade = quantidade >= 0 ? quantidade : 0;
                    }
                    else
                    {
                        quantidade = (decimal)((consumoMedio * tempoReposicao) + estoqueMaximo - estoque);
                        itemCompraViewDto.Quantidade = quantidade >= 0 ? quantidade : 0;
                    }

                    if(filtroCompraDto.SaldoQuantidadeComprometida is not null && filtroCompraDto.SaldoQuantidadeComprometida.Value)
                    {
                        itemCompraViewDto.Quantidade += quantidadeComprometida;
                    }

                    itemCompraViewDto.ProdutoId = produto.Id;
                    itemCompraViewDto.DescricaoProduto = produto.Descricao;

                    if (produto.Laboratorio is not null) itemCompraViewDto.DescricaoLaboratorio = produto.Laboratorio.Descricao;

                    itemCompraViewDto.Comprar = itemCompraViewDto.Quantidade > 0;
                    itemCompraViewDto.CodigoBarras = produto.CodigoBarra;
                    itemCompraViewDto.CodigoCas = produto.CodigoCas;
                    itemCompraViewDto.CodigoDcb = produto.CodigoDcb;
                    itemCompraViewDto.CurvaAbc = itemCompra.CurvaAbc;
                    itemCompraViewDto.GrupoId = itemCompra.GrupoId;
                    itemCompraViewDto.Estoque = estoque;
                    itemCompraViewDto.EstoqueMinimo = estoqueMinimo;
                    itemCompraViewDto.EstoqueMaximo = estoqueMaximo;
                    itemCompraViewDto.QuantidadeVendida = itemCompra.QuantidadeVendida is not null ? itemCompra.QuantidadeVendida.Value : 0;

                    if (itemCompraViewDto.Quantidade > 0)
                    {
                        itemCompraViewDto.ValorUnitario = itemCompra.ValorCusto;
                        itemCompraViewDto.ValorTotal = itemCompraViewDto.ValorUnitario * itemCompraViewDto.Quantidade;
                    }

                    var nfEntrada = await _notaFiscalDeEntradaRepository.GetUltimaCompra(produto.Id, filtroCompraDto.EmpresaId.Value);

                    if (nfEntrada is not null)
                    {
                        if (nfEntrada.Fornecedor is not null)
                        {
                            itemCompraViewDto.NomeFornecedor = nfEntrada.Fornecedor.NomeFornecedor;
                            itemCompraViewDto.Fornecedores.Add(nfEntrada.Fornecedor);
                        }
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

                    itensCompraViewDto.Add(itemCompraViewDto);

                };

                return itensCompraViewDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
