using Ms_Compras.Database.Entidades;
using Ms_Compras.Dtos;

namespace Ms_Compras.Services.Interfaces
{
    public interface IGerarArquivosEmbraFarmaService
    {
        Task<ArquivosCotacaoCompra> GerarArquivo(CotacaoCompraEmbraFarmaDto cotacaoCompraEmbraFarmaDto,DadosToken dadosToken);
    }
}