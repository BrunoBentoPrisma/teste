using AutoMapper;
using Ms_Compras.Database.Entidades;
using Ms_Compras.Database.Interfaces;
using Ms_Compras.Dtos;
using Ms_Compras.Dtos.CompraFornecedor;
using Ms_Compras.Services.Interfaces;
using Ms_Compras.RabbitMq.Producer.Interfaces;

namespace Ms_Compras.Services.Service
{
    public class CompraFornecedorService : ICompraFornecedorService
    {
        private readonly ICompraFornecedorRepository _compraFornecedorRepository;
        private readonly ICompraFornecedorProducer _compraFornecedorProducer;
        private readonly IMapper _mapper;
        public CompraFornecedorService(ICompraFornecedorRepository compraFornecedorRepository,
            IMapper mapper,
            ICompraFornecedorProducer compraFornecedorProducer
            )
        {
            _mapper = mapper;
            _compraFornecedorProducer = compraFornecedorProducer;
            _compraFornecedorRepository = compraFornecedorRepository;
        }

        public async Task<bool> AdicionarCompraFornecedorAsync(Compra compra, List<Guid> fornecedoresIds)
        {
            try
            {
                for (int i = 0; i < fornecedoresIds.Count; i++)
                {
                    var compraFornecedor = _mapper.Map<CompraFornecedor>(compra);

                    compraFornecedor.Fornecedor = null;
                    compraFornecedor.FornecedorId = fornecedoresIds[i];
                    compraFornecedor.CompraId = compra.Id;
                    compraFornecedor.Id = new Guid();

                    compra.ItensCompras.ForEach(itens =>
                    {
                        var itemFornecedor = _mapper.Map<ItensCompraFornecedor>(itens);
                        itemFornecedor.Id = new Guid();
                        itemFornecedor.CompraFornecedorId = compraFornecedor.Id;
                        compraFornecedor.ItensCompraFornecedor.Add(itemFornecedor);
                    });

                    var result = await _compraFornecedorRepository.AdicionarAsync(compraFornecedor);

                    _compraFornecedorProducer.CompraFornecedorMessage(result, "AdicionarCompraFornecedor");
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> AdicionarCompraFornecedorAsync(CompraFornecedorCreateDto compraFornecedorCreateDto, DadosToken dadosToken)
        {
            try
            {
                var compraFornecedor = _mapper.Map<CompraFornecedor>(compraFornecedorCreateDto);

                compraFornecedor.EmpresaId = dadosToken.EmpresaId;
                compraFornecedor.NomeCriador = dadosToken.Nome;

                var result = await _compraFornecedorRepository.AdicionarAsync(compraFornecedor);

                _compraFornecedorProducer.CompraFornecedorMessage(result, "AdicionarCompraFornecedor");

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<bool> EditarCompraFornecedorAsync(Compra compra)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> EditarStatusCompraFornecedor(EditarStatusDto editarStatusDto, DadosToken dadosToken)
        {
            try
            {

                if(editarStatusDto.DataPreveEntrega is null && editarStatusDto.StatusPedido is null) throw new Exception();

                var compraFornecedor = await _compraFornecedorRepository.GetByIdAsync(editarStatusDto.Id, dadosToken.EmpresaId);

                if(compraFornecedor is null) throw new Exception($"Não foi possível localizar a compra fornecedor com id :{editarStatusDto.Id}");

                compraFornecedor.Fornecedor = null;
                compraFornecedor.ItensCompraFornecedor = null;
                compraFornecedor.DataDeAlteracao = DateTime.Now;
                compraFornecedor.NomeEditor = dadosToken.Nome;
                if(editarStatusDto.StatusPedido is not null) compraFornecedor.StatusPedido = editarStatusDto.StatusPedido.Value;
                if(editarStatusDto.DataPreveEntrega is not null) compraFornecedor.DataPreveEntrega = editarStatusDto.DataPreveEntrega.Value;

                var result = await _compraFornecedorRepository.EditarAsync(compraFornecedor);

                _compraFornecedorProducer.CompraFornecedorMessage(compraFornecedor, "EditarStatusCompraFornecedor");

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task ExcluirCompraFornecedorByIdAsync(Guid id, Guid empresaId)
        {
            try
            {
                var compraFornecedor = await _compraFornecedorRepository.GetByIdAsync(id, empresaId);

                if (compraFornecedor == null) throw new Exception();

                compraFornecedor.Excluido = true;
                compraFornecedor.DataDeExclusao = DateTime.Now;

                await _compraFornecedorRepository.ExcluirAsync(compraFornecedor);

                _compraFornecedorProducer.CompraFornecedorMessage(compraFornecedor, "ExcluirCompraFornecedor");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ExcluirCompraFornecedorByIdCompraAsync(Guid compraId, DadosToken dadosToken)
        {
            try
            {
                var comprasFornecedores = await _compraFornecedorRepository.GetListByCompraIdAsync(compraId, dadosToken.EmpresaId);

                if (comprasFornecedores == null) throw new Exception();

                comprasFornecedores.ForEach(async compraFornecedor =>
                {
                    compraFornecedor.DataDeExclusao = DateTime.Now;
                    compraFornecedor.Excluido = true;

                    await _compraFornecedorRepository.ExcluirAsync(compraFornecedor);

                    _compraFornecedorProducer.CompraFornecedorMessage(compraFornecedor, "ExcluirCompraFornecedor");
                });

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CompraFornecedorViewDto> GetByIdAsync(Guid id, Guid empresaId)
        {
            try
            {
                var compraFornecedor = await _compraFornecedorRepository.GetByIdAsync(id, empresaId);

                if (compraFornecedor == null) throw new Exception();

                return _mapper.Map<CompraFornecedorViewDto>(compraFornecedor);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<CompraFornecedorViewDto>> GetListByCompraViewIdAsync(Guid id, Guid empresaId)
        {
            try
            {
                var compraFornecedor = await _compraFornecedorRepository.GetListByCompraIdAsync(id, empresaId);

                if (compraFornecedor == null) throw new Exception();

                var compraFornecedorViewDto = _mapper.Map<List<CompraFornecedorViewDto>>(compraFornecedor);

                return compraFornecedorViewDto;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
