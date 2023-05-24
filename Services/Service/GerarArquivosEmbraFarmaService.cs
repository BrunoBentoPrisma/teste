using Ms_Compras.Database.Entidades;
using Ms_Compras.Database.Interfaces;
using Ms_Compras.Dtos;
using Ms_Compras.RabbitMq.Producer.Interfaces;
using Ms_Compras.Services.Interfaces;

namespace Ms_Compras.Services.Service
{
    public class GerarArquivosEmbraFarmaService : IGerarArquivosEmbraFarmaService
    {
        private readonly ICompraFornecedorRepository _compraFornecedorRepository;
        private readonly IGenericRepository<ArquivosCotacaoCompra> _genericRepository;
        private readonly IEmailEmbraFarmaProducer _emailEmbraFarmaProducer;

        public GerarArquivosEmbraFarmaService(ICompraFornecedorRepository compraFornecedorRepository,
            IGenericRepository<ArquivosCotacaoCompra> genericRepository,
            IEmailEmbraFarmaProducer emailEmbraFarmaProducer
            )
        {
            _emailEmbraFarmaProducer = emailEmbraFarmaProducer;
            _genericRepository = genericRepository;
            _compraFornecedorRepository = compraFornecedorRepository;
        }

        public async Task<ArquivosCotacaoCompra> GerarArquivo(CotacaoCompraEmbraFarmaDto cotacaoCompraEmbraFarmaDto, DadosToken dadosToken)
        {
            try
            {
                ArquivosCotacaoCompra arquivoCotacaoCompra = new ArquivosCotacaoCompra();

                var compraFornecedor = new CompraFornecedor();

                cotacaoCompraEmbraFarmaDto.Ids.ForEach(async id =>
                {
                    var compraFornecedorUpdate = await _compraFornecedorRepository.GetByIdAsync(id, dadosToken.EmpresaId);

                    compraFornecedorUpdate.DataDeAlteracao = DateTime.Now;
                    compraFornecedorUpdate.NomeEditor = dadosToken.Nome;
                    compraFornecedorUpdate.StatusCotacao = StatusCotacao.Emitida;

                    compraFornecedor = await _compraFornecedorRepository.EditarAsync(compraFornecedorUpdate);
                });

                arquivoCotacaoCompra = new ArquivosCotacaoCompra();

                using (MemoryStream ms = new MemoryStream())
                {
                    using (StreamWriter writer = new StreamWriter(ms))
                    {
                        //writer.WriteLine($"{dadosToken.EmpresaId};{empresa.Cnpj};{compraFornecedor.CompraId};3.2"); //"3.2" = versão do layouyt do arquivo dado chumbado
                        writer.WriteLine($"{dadosToken.EmpresaId};{compraFornecedor.CompraId};3.2");
                        int contador = 0;

                        compraFornecedor.ItensCompraFornecedor.ForEach(itensCompraFornecedor =>
                        {
                            var produtoDescricao = itensCompraFornecedor.Produto.Descricao.Length > 30
                                ? itensCompraFornecedor.Produto.Descricao.Substring(30)
                                : itensCompraFornecedor.Produto.Descricao;

                            var observacao = string.Empty;

                            writer.WriteLine($"3" +
                                $"{contador.ToString().PadRight(6, '0')}" +
                                $"{itensCompraFornecedor.Produto.CodigoDcb.PadRight(15)}" +
                                $"{itensCompraFornecedor.Produto.CodigoCas.PadRight(15)}" +
                                $"{itensCompraFornecedor.Produto.CodigoBarra.PadRight(13)}" +
                                $"{dadosToken.EmpresaId.ToString().PadLeft(6, '0')}" +
                                $"{produtoDescricao.PadRight(30)}" +
                                $"{itensCompraFornecedor.QuantidadeCompra.ToString("0.0000").Replace(",", "").PadLeft(15, '0')}" +
                                $"{itensCompraFornecedor.Produto.UnidadeEstoque.PadRight(3)}" +
                                $"{itensCompraFornecedor.Produto.FornecedorId.ToString().PadRight(30)}" +
                                $"000000000000000   000000000000000                       0000000000{observacao.PadRight(60)}");

                            contador++;
                        });

                        writer.WriteLine($"9;{compraFornecedor.ItensCompraFornecedor.Count}");

                        writer.Flush();

                        ms.Position = 0;
                        
                        //Envio Cotação Compra - {empresa.Nome}

                        var message = new MessageEmailDto(
                            dadosToken.EmpresaId,
                            cotacaoCompraEmbraFarmaDto.Destinatario,
                            "Envio Cotação Compra -"
                            , cotacaoCompraEmbraFarmaDto.Mensagem
                            , cotacaoCompraEmbraFarmaDto.Copia,
                            "CotacaoCompra.txt",
                            "text/plain",
                            ms
                            );

                        _emailEmbraFarmaProducer.EnviarEmail(message, "EnviarEmailEmbraFarma");

                        arquivoCotacaoCompra.NomeCriador = dadosToken.Nome;
                        arquivoCotacaoCompra.EmbraFarma = null;
                        arquivoCotacaoCompra.EmbraFarma = null;
                        arquivoCotacaoCompra.EmbraFarma = ms.ToArray();
                        arquivoCotacaoCompra.CompraId = compraFornecedor.CompraId;
                        arquivoCotacaoCompra.EmbraFarmaString = System.Text.Encoding.UTF8.GetString(arquivoCotacaoCompra.EmbraFarma);

                        await _genericRepository.AdicionarAsync(arquivoCotacaoCompra);

                        return arquivoCotacaoCompra;

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