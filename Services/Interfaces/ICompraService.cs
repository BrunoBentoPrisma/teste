using Ms_Compras.Database.Entidades;
using Ms_Compras.Dtos;
using Ms_Compras.Dtos.Compra;

namespace Ms_Compras.Services.Interfaces
{
    public interface ICompraService
    {
        Task<bool> AdicionarCompraAsync(CompraCreateDto compraCreateDto, DadosToken dadosToken);
        Task<bool> EditarCompraAsync(Compra compra, DadosToken dadosToken);
        Task<CompraViewDto> GetByIdAsync(Guid id, Guid empresaId);
        Task<List<CompraViewDto>> GetAllAsync(Guid empresaId);
        Task<PaginacaoDto<CompraViewDto>> GetPaginacaoAsync(PaginacaoRequestDto paginacaoRequestDto);
        Task<bool> ExcluirCompraAsync(Guid id, DadosToken dadosToken);
    }
}
