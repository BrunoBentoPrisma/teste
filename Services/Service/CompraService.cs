using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ms_Compras.Database.Entidades;
using Ms_Compras.Database.Interfaces;
using Ms_Compras.Dtos;
using Ms_Compras.Dtos.Compra;
using Ms_Compras.RabbitMq.Producer.Interfaces;
using Ms_Compras.Services.Interfaces;

namespace Ms_Compras.Services.Service
{
    public class CompraService : ICompraService
    {
        private readonly ICompraRepository _compraRepository;
        private readonly IMapper _mapper;
        private readonly ICompraFornecedorService _compraFornecedorService;
        private readonly ICompraProducer _compraProducer;

        public CompraService(ICompraRepository compraRepository,
            IMapper mapper,
            ICompraFornecedorService compraFornecedorService,
            ICompraProducer compraProducer
            )
        {
            _compraProducer = compraProducer;
            _compraFornecedorService = compraFornecedorService;
            _mapper = mapper;
            _compraRepository = compraRepository;
        }

        public async Task<bool> AdicionarCompraAsync(CompraCreateDto compraCreateDto, DadosToken dadosToken)
        {
            try
            {
                if (compraCreateDto == null) throw new Exception("Compra com parâmetros inválidos !");

                var compra = _mapper.Map<Compra>(compraCreateDto);

                compra.EmpresaId = dadosToken.EmpresaId;
                compra.NomeCriador = dadosToken.Nome;
                compra.ItensCompras = _mapper.Map<List<ItensCompra>>(compraCreateDto.ItensCompras);

                compra.ItensCompras.ForEach(itens =>
                {
                    compra.TotalCompra += itens.ValorTotal;
                    itens.EmpresaId = dadosToken.EmpresaId;
                    itens.NomeCriador = dadosToken.Nome;
                    itens.Produto = null;
                });

                var result = await _compraRepository.AdicionarAsync(compra);

                _compraProducer.CompraMessage(result, "AdicionarCompra");

                await _compraFornecedorService.AdicionarCompraFornecedorAsync(compra, compraCreateDto.FornecedoresIds);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<bool> EditarCompraAsync(Compra compra, DadosToken dadosToken)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExcluirCompraAsync(Guid id, DadosToken dadosToken)
        {
            try
            {
                var compra = await _compraRepository.GetByIdAsync(id, dadosToken.EmpresaId);

                if (compra == null) throw new Exception();

                compra.DataDeExclusao = DateTime.Now;
                compra.Excluido = true;

                await _compraRepository.ExcluirAsync(compra);

                _compraProducer.CompraMessage(compra, "ExcluirCompra");

                await _compraFornecedorService.ExcluirCompraFornecedorByIdCompraAsync(compra.Id, dadosToken);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<CompraViewDto>> GetAllAsync(Guid empresaId)
        {
            try
            {
                var compra = await _compraRepository.GetAllAsync(empresaId);

                if (compra == null) throw new Exception();

                var compraViewDto = _mapper.Map<List<CompraViewDto>>(compra);

                return compraViewDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CompraViewDto> GetByIdAsync(Guid id, Guid empresaId)
        {
            try
            {
                var compra = await _compraRepository.GetByIdAsync(id, empresaId);

                if (compra == null) throw new Exception();

                var compraViewDto = _mapper.Map<CompraViewDto>(compra);
                compraViewDto.ItensCompras = _mapper.Map<List<ItensCompraViewDto>>(compra.ItensCompras);

                return compraViewDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PaginacaoDto<CompraViewDto>> GetPaginacaoAsync(PaginacaoRequestDto paginacaoRequestDto)
        {
            try
            {
                Expression<Func<Compra, bool>> predicateWhere = !string.IsNullOrEmpty(paginacaoRequestDto.Search.Trim()) ?
                        x => !x.Excluido && x.EmpresaId == paginacaoRequestDto.EmpresaId :
                        x => !x.Excluido && x.EmpresaId == paginacaoRequestDto.EmpresaId;

                var paginacao = new PaginacaoDto<CompraViewDto>();
                var paginacaoEstado = new PaginacaoDto<Compra>();

                paginacaoRequestDto.SortBy = !string.IsNullOrEmpty(paginacaoRequestDto.SortBy.Trim())
                    ? paginacaoRequestDto.SortBy.Substring(0, 1).ToUpper() + paginacaoRequestDto.SortBy.Substring(1)
                    : "DataDeCadastro";

                paginacaoEstado =   await _compraRepository.GetPaginacaoAsync(paginacaoRequestDto,
                                                        GetExpression(paginacaoRequestDto.SortBy),
                                                        predicateWhere, null);

                paginacao.Values = _mapper.Map<List<CompraViewDto>>(paginacaoEstado.Values);

                paginacao.PageCount = paginacaoEstado.PageCount;

                return paginacao;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private Expression<Func<Compra, string>> GetExpression(string sortBy)
        {
            switch (sortBy)
            {
                case "StatusCompra":
                    return x => x.StatusCompra.ToString();
                case "TipoCompra":
                    return x => x.TipoCompra.ToString();
                case "CurvaAbc":
                    return x => x.CurvaAbc.ToString();
                default:
                    return x => x.DataDeCadastro.ToString();
            }
        }
    }
}
