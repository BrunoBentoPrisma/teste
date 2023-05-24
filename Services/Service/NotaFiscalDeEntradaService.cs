using Ms_Compras.Database.Interfaces;
using Ms_Compras.Dtos.NotaFiscalDeEntrada;
using Ms_Compras.Services.Interfaces;

namespace Ms_Compras.Services.Service
{
    public class NotaFiscalDeEntradaService : INotaFiscalDeEntradaService
    {
        private readonly INotaFiscalDeEntradaRepository _notaFiscalDeEntradaRepository;

        public NotaFiscalDeEntradaService(INotaFiscalDeEntradaRepository notaFiscalDeEntradaRepository)
        {
            _notaFiscalDeEntradaRepository = notaFiscalDeEntradaRepository;
        }

        public async Task<List<UltimasComprasDto>> GetUltimasComprasPorPeriodo(FiltroUltimasCompras filtroUltimasCompras, Guid empresaId)
        {
            try
            {
                var ultimasCompraDto = new List<UltimasComprasDto>();

                var ultimasCompras = await _notaFiscalDeEntradaRepository.GetUltimasComprasPorPeriodo(filtroUltimasCompras, empresaId);

                if (ultimasCompras is null) throw new Exception($"Não foi possível filtrar as ultimas compras do produto com id : {filtroUltimasCompras.ProdutoId}");

                ultimasCompras.ForEach(ultimaCompra =>
                {
                    ultimaCompra.ItensNotaFiscalDeEntrada.ForEach(item =>
                    {
                        var ultimaCompraDto = new UltimasComprasDto();

                        ultimaCompraDto.DataDeCadastro = ultimaCompra.DataDeCadastro;
                        ultimaCompraDto.DataDeEmissao = ultimaCompra.DataDeEmissao;
                        ultimaCompraDto.NomeFornecedor = ultimaCompra.Fornecedor.NomeFornecedor;
                        ultimaCompraDto.NumeroNota = ultimaCompra.NumeroNota;
                        ultimaCompraDto.SerieNota = ultimaCompra.SerieNota;
                        ultimaCompraDto.ValorIpi = item.ValorIpi;
                        ultimaCompraDto.QuantidadeUnidadeEstoque = item.Quantidade;
                        ultimaCompraDto.SiglaUnidade = item.Produto.UnidadeEstoque;
                        ultimaCompraDto.ValorFreteItem = item.Frete;
                        ultimaCompraDto.Total = item.Total;
                        ultimaCompraDto.Custo = item.Produto.ValorCusto;

                        ultimasCompraDto.Add(ultimaCompraDto);

                    });
                });

                return ultimasCompraDto;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
