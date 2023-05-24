using Ms_Compras.Database.Interfaces;
using Ms_Compras.Dtos;
using Ms_Compras.Dtos.CompraFornecedor;
using Ms_Compras.Services.Interfaces;

namespace Ms_Compras.Services.Service
{
    public class ItemCompraFornecedorService : IItemCompraFornecedorService
    {
        private readonly IItemCompraFornecedorRepository _itemCompraFornecedorRepository;

        public ItemCompraFornecedorService(IItemCompraFornecedorRepository itemCompraFornecedorRepository)
        {
            _itemCompraFornecedorRepository = itemCompraFornecedorRepository;
        }

        public async Task<bool> EditarStatusItem(EditarStatusItemDto editarStatusItemDto, DadosToken dadosToken)
        {
            try
            {
                var item = await _itemCompraFornecedorRepository.GetItem(editarStatusItemDto.Id);

                if(item is null) throw new Exception($"Não foi possível localizar o item da compra com Id :{editarStatusItemDto.Id}");

                item.StatusItemPedido = editarStatusItemDto.StatusItem;
                item.DataDeAlteracao = DateTime.Now;
                item.NomeEditor = dadosToken.Nome;

                await _itemCompraFornecedorRepository.EditarAsync(item);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}