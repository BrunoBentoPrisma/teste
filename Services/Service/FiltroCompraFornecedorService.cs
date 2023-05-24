using AutoMapper;
using Ms_Compras.Database.Interfaces;
using Ms_Compras.Dtos.CompraFornecedor;
using Ms_Compras.Services.Interfaces;

namespace Ms_Compras.Services.Service
{
    public class FiltroCompraFornecedorService : IFiltroCompraFornecedorService
    {
        private readonly ICompraFornecedorRepository _compraFornecedorRepository;
        private readonly IMapper _mapper;

        public FiltroCompraFornecedorService(ICompraFornecedorRepository compraFornecedorRepository, IMapper mapper)
        {
            _compraFornecedorRepository = compraFornecedorRepository;
            _mapper = mapper;
        }

        public async Task<List<CompraFornecedorViewDto>> ConsultarPedidoCompraFornecedor(FiltroPedidoCompraFornecedor filtroPedido, Guid empresaId)
        {
            try
            {
                var filtros = await _compraFornecedorRepository.ConsultarPedido(filtroPedido, empresaId);

                if (filtros is null) throw new Exception($"Não foi possível filtrar as compras com Id : {filtroPedido.CompraId}");

                var filtrosDto = _mapper.Map<List<CompraFornecedorViewDto>>(filtros);

                return filtrosDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<CompraFornecedorViewDto>> GetFiltroCotacaoCompra(FiltroCotacaoCompraDto filtroCotacaoCompraDto, Guid empresaId)
        {
            try
            {
                var filtros = await _compraFornecedorRepository.GetFiltroCotacaoCompras(filtroCotacaoCompraDto, empresaId);

                if (filtros is null) throw new Exception($"Não foi possível filtrar as cotações da compra : {filtroCotacaoCompraDto.CompraId}");

                var filtroDto = _mapper.Map<List<CompraFornecedorViewDto>>(filtros);

                return filtroDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
