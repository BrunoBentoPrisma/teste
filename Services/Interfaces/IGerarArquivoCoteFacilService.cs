using Ms_Compras.Database.Entidades;
using Ms_Compras.Dtos;

namespace Ms_Compras.Services.Interfaces
{
    public interface IGerarArquivoCoteFacilService
    {
        Task<ArquivosCotacaoCompra> GerarArquivo(List<Guid> ids,DadosToken dadosToken);
    }
}