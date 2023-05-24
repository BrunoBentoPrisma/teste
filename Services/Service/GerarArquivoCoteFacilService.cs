using Ms_Compras.Database.Entidades;
using Ms_Compras.Database.Interfaces;
using Ms_Compras.Dtos;
using Ms_Compras.Services.Interfaces;

namespace Ms_Compras.Services.Service
{
    public class GerarArquivoCoteFacilService : IGerarArquivoCoteFacilService
    {
        private readonly ICompraFornecedorRepository _compraFornecedorRepository;
        private readonly IGenericRepository<ArquivosCotacaoCompra> _genericRepository;

        public GerarArquivoCoteFacilService(ICompraFornecedorRepository compraFornecedorRepository,
            IGenericRepository<ArquivosCotacaoCompra> genericRepository
            )
        {
            _genericRepository = genericRepository;
            _compraFornecedorRepository = compraFornecedorRepository;
        }

        public async Task<ArquivosCotacaoCompra> GerarArquivo(List<Guid> ids, DadosToken dadosToken)
        {
            try
            {
                ArquivosCotacaoCompra arquivoCotacaoCompra = new ArquivosCotacaoCompra();


                var compraFornecedor = new CompraFornecedor();

                ids.ForEach(async id =>
                {
                    var compraFornecedorUpdate = await _compraFornecedorRepository.GetByIdAsync(id, dadosToken.EmpresaId);

                    compraFornecedorUpdate.DataDeAlteracao = DateTime.Now;
                    compraFornecedorUpdate.NomeEditor = dadosToken.Nome;
                    compraFornecedorUpdate.StatusCotacao = StatusCotacao.Emitida;

                    compraFornecedor = await _compraFornecedorRepository.EditarAsync(compraFornecedorUpdate);
                });

                using (MemoryStream ms = new MemoryStream())
                {
                    using (StreamWriter writer = new StreamWriter(ms))
                    {
                        //writer.WriteLine($"{dadosToken.EmpresaId};{empresa.Cnpj};{compraFornecedor.CompraId};3.2"); //"3.2" = versão do layouyt do arquivo dado chumbado
                        writer.WriteLine($"{dadosToken.EmpresaId};{compraFornecedor.CompraId};3.2");

                        compraFornecedor.ItensCompraFornecedor.ForEach(itensCompraFornecedor =>
                        {
                            var nomeFornecedor = itensCompraFornecedor.Produto.Fornecedor != null ? itensCompraFornecedor.Produto.Fornecedor.NomeFornecedor : "";
                            writer.WriteLine($"2;{itensCompraFornecedor.Produto.CodigoBarra};" +
                            $"{itensCompraFornecedor.QuantidadeCompra};" +
                            $"{itensCompraFornecedor.Produto.Id};" +
                            $"{itensCompraFornecedor.Produto.Descricao};" +
                            $"{nomeFornecedor};" +
                            $"{itensCompraFornecedor.ValorUnitario};" +
                            $"{itensCompraFornecedor.Produto.ValorCusto}" +
                            $";0;0" + // Verificar essas informações
                            $"{itensCompraFornecedor.Produto.CurvaAbc}");
                        });

                        writer.WriteLine($"9;{compraFornecedor.ItensCompraFornecedor.Count}");

                        writer.Flush();

                        ms.Position = 0;

                        arquivoCotacaoCompra.NomeCriador = dadosToken.Nome;
                        arquivoCotacaoCompra.EmbraFarma = null;
                        arquivoCotacaoCompra.CoteFacil = ms.ToArray();
                        arquivoCotacaoCompra.CompraId = compraFornecedor.CompraId;
                        arquivoCotacaoCompra.CoteFacilString = System.Text.Encoding.UTF8.GetString(arquivoCotacaoCompra.CoteFacil);

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