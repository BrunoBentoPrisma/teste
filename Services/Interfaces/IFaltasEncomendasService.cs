using Ms_Compras.Dtos;
using Ms_Compras.Dtos.Compra;
using Ms_Compras.Dtos.FaltasEncomendas;

namespace Ms_Compras.Services.Interfaces
{
    public interface IFaltasEncomendasService
    {
        bool AdicionarFaltasEncomendasAsync(List<FaltasEncomendasCreateDto> faltasEncomendasAddDto, DadosToken dadosToken);
        Task<bool> EditarFaltasEncomendasAsync(FaltasEncomendasEditDto faltasEncomendasEditDto, DadosToken dadosToken);
        Task<FaltasEncomendasViewDto> GetByIdAsync(Guid id, Guid empresaId);
        Task<List<FaltasEncomendasViewDto>> GetAllAsync(Guid empresaId);
        Task<PaginacaoDto<FaltasEncomendasViewDto>> GetPaginacaoAsync(PaginacaoRequestDto paginacaoRequestDto);
        Task<bool> ExcluirFaltasEncomendasAsync(Guid id, DadosToken dadosToken);
        Task<bool> ConcluirFaltasEncomendasAsync(Guid id, DadosToken dadosToken);
    }
}