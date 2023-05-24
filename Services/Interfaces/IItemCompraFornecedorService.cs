using Ms_Compras.Dtos;
using Ms_Compras.Dtos.CompraFornecedor;

namespace Ms_Compras.Services.Interfaces
{
    public interface IItemCompraFornecedorService
    {
        Task<bool> EditarStatusItem(EditarStatusItemDto editarStatusItemDto, DadosToken dadosToken);
    }
}