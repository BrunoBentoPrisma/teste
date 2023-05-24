using Ms_Compras.Database.Entidades;
using Ms_Compras.Dtos;
using Ms_Compras.Dtos.CompraFornecedor;

namespace Ms_Compras.Services.Interfaces
{
    public interface ICompraFornecedorService
    {
        Task<bool> AdicionarCompraFornecedorAsync(Compra compra, List<Guid> fornecedoresIds);
        Task<bool> AdicionarCompraFornecedorAsync(CompraFornecedorCreateDto compraFornecedorCreateDto, DadosToken dadosToken);
        Task<bool> EditarCompraFornecedorAsync(Compra compra);
        Task<bool> EditarStatusCompraFornecedor(EditarStatusDto editarStatusDto, DadosToken dadosToken);
        Task ExcluirCompraFornecedorByIdAsync(Guid id, Guid empresaId);
        Task<bool> ExcluirCompraFornecedorByIdCompraAsync(Guid compraId, DadosToken dadosToken);
        Task<CompraFornecedorViewDto> GetByIdAsync(Guid id, Guid empresaId);
        Task<List<CompraFornecedorViewDto>> GetListByCompraViewIdAsync(Guid id, Guid empresaId);
    }
}
