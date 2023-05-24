using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ms_Compras.Database.Entidades;
using Ms_Compras.Database.Interfaces;
using Ms_Compras.Dtos;
using Ms_Compras.Dtos.FaltasEncomendas;
using Ms_Compras.RabbitMq.Producer.Interfaces;
using Ms_Compras.Services.Interfaces;

namespace Ms_Compras.Services.Service
{
    public class FaltasEncomendasService : IFaltasEncomendasService
    {
        private readonly IMapper _mapper;
        private readonly IFaltasEncomendasRepository _faltasEncomendasRepository;
        private readonly IFaltasEncomendasProducer _faltasEncomendasProducer;

        public FaltasEncomendasService(IFaltasEncomendasRepository faltasEncomendasRepository,
                IMapper mapper,
                IFaltasEncomendasProducer faltasEncomendasProducer
            )
        {
            _faltasEncomendasProducer = faltasEncomendasProducer;
            _faltasEncomendasRepository = faltasEncomendasRepository;
            _mapper = mapper;
        }

        public bool AdicionarFaltasEncomendasAsync(List<FaltasEncomendasCreateDto> faltasEncomendasAddDto, DadosToken dadosToken)
        {
            try
            {

                var validQuantidadeZero = faltasEncomendasAddDto.Any(x => x.Quantidade == 0);

                if (validQuantidadeZero) throw new ArgumentException("Quantidade do produtos inválidas !");

                var faltasEncomendas = _mapper.Map<List<FaltasEncomendas>>(faltasEncomendasAddDto);

                faltasEncomendas.ForEach(async faltaEncomenda =>
                {

                    faltaEncomenda.EmpresaId = dadosToken.EmpresaId;
                    faltaEncomenda.NomeCriador = dadosToken.Nome;

                    var result = await _faltasEncomendasRepository.AdicionarAsync(faltaEncomenda);

                    _faltasEncomendasProducer.FaltasEncomendasMessage(result, "AdicionarFaltasEncomendas");

                });

                return true;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ConcluirFaltasEncomendasAsync(Guid id, DadosToken dadosToken)
        {
            try
            {
                var faltaEncomenda = await _faltasEncomendasRepository.GetByIdAsync(id, dadosToken.EmpresaId);

                if (faltaEncomenda is null) throw new Exception($"Não foi possível retornar a falta encomenda com id :{id}");

                faltaEncomenda.Produto = null;
                faltaEncomenda.Grupo = null;
                faltaEncomenda.DataDeAlteracao = DateTime.Now;
                faltaEncomenda.NomeEditor = dadosToken.Nome;
                faltaEncomenda.Status = 2;

                await _faltasEncomendasRepository.EditarAsync(faltaEncomenda);

                _faltasEncomendasProducer.FaltasEncomendasMessage(faltaEncomenda, "EditarFaltasEncomendas");

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> EditarFaltasEncomendasAsync(FaltasEncomendasEditDto faltasEncomendasEditDto, DadosToken dadosToken)
        {
            try
            {
                var validQuantidadeZero = faltasEncomendasEditDto.FaltasEncomendas.Any(x => x.Quantidade == 0);

                if (validQuantidadeZero) throw new ArgumentException("Quantidade do produtos inválidas !");

                var faltasEncomendaExcluir = await _faltasEncomendasRepository.GetByIdAsync(faltasEncomendasEditDto.Id, dadosToken.EmpresaId);

                if (faltasEncomendaExcluir == null) throw new Exception($"Não foi possível localizar a falta/encomenda com Id :{faltasEncomendasEditDto.Id}");

                faltasEncomendaExcluir.Excluido = true;
                faltasEncomendaExcluir.DataDeExclusao = DateTime.Now;

                await _faltasEncomendasRepository.ExcluirAsync(faltasEncomendaExcluir);

                var faltasencomendas = _mapper.Map<List<FaltasEncomendas>>(faltasEncomendasEditDto.FaltasEncomendas);

                faltasencomendas.ForEach(async faltaEncomenda =>
                {
                    faltaEncomenda.DataDeCadastro = faltasEncomendaExcluir.DataDeCadastro;
                    faltaEncomenda.EmpresaId = dadosToken.EmpresaId;
                    faltaEncomenda.DataDeAlteracao = DateTime.Now;
                    faltaEncomenda.NomeCriador = faltasEncomendaExcluir.NomeCriador;
                    faltaEncomenda.NomeEditor = dadosToken.Nome;

                    var result = await _faltasEncomendasRepository.AdicionarAsync(faltaEncomenda);

                    _faltasEncomendasProducer.FaltasEncomendasMessage(result, "EditarFaltasEncomendas");
                });

                return true;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ExcluirFaltasEncomendasAsync(Guid id, DadosToken dadosToken)
        {
            try
            {
                var faltasEncomendaExcluir = await _faltasEncomendasRepository.GetByIdAsync(id, dadosToken.EmpresaId);

                if (faltasEncomendaExcluir == null) return false;

                faltasEncomendaExcluir.Excluido = true;
                faltasEncomendaExcluir.DataDeExclusao = DateTime.Now;

                await _faltasEncomendasRepository.ExcluirAsync(faltasEncomendaExcluir);

                _faltasEncomendasProducer.FaltasEncomendasMessage(faltasEncomendaExcluir, "ExcluirFaltasEncomendas");

                return true;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<FaltasEncomendasViewDto>> GetAllAsync(Guid empresaId)
        {
            try
            {

                var faltasEncomendas = await _faltasEncomendasRepository.GetAllAsync(empresaId);

                if (faltasEncomendas == null) throw new Exception();

                return _mapper.Map<List<FaltasEncomendasViewDto>>(faltasEncomendas);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<FaltasEncomendasViewDto> GetByIdAsync(Guid id, Guid empresaId)
        {
            try
            {

                var faltasEncomendas = await _faltasEncomendasRepository.GetByIdAsync(id, empresaId);

                if (faltasEncomendas == null) throw new Exception();

                return _mapper.Map<FaltasEncomendasViewDto>(faltasEncomendas);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PaginacaoDto<FaltasEncomendasViewDto>> GetPaginacaoAsync(PaginacaoRequestDto paginacaoRequestDto)
        {
            try
            {
                Expression<Func<FaltasEncomendas, bool>> predicateWhere = !string.IsNullOrEmpty(paginacaoRequestDto.Search.Trim()) ?
                                                                x => !x.Excluido && x.EmpresaId == paginacaoRequestDto.EmpresaId &&
                                                                (EF.Functions.ILike(EF.Functions.Unaccent(x.Produto.Descricao), $"%{paginacaoRequestDto.Search}%") ||
                                                                EF.Functions.ILike(EF.Functions.Unaccent(x.Vendedor.Nome), $"%{paginacaoRequestDto.Search}%")) :
                                                                x => !x.Excluido && x.EmpresaId == paginacaoRequestDto.EmpresaId;

                var paginacao = new PaginacaoDto<FaltasEncomendasViewDto>();
                var paginacaoEstado = new PaginacaoDto<FaltasEncomendas>();

                paginacaoRequestDto.SortBy = !string.IsNullOrEmpty(paginacaoRequestDto.SortBy.Trim())
                    ? paginacaoRequestDto.SortBy.Substring(0, 1).ToUpper() + paginacaoRequestDto.SortBy.Substring(1)
                    : "DataDeCadastro";

                List<Expression<Func<FaltasEncomendas, object>>>? predicateInclude = new List<Expression<Func<FaltasEncomendas, object>>>() 
                {
                    x => x.Produto,
                    x => x.Vendedor
                };

                paginacaoEstado = await _faltasEncomendasRepository.GetPaginacaoAsync(paginacaoRequestDto,
                                                        GetExpression(paginacaoRequestDto.SortBy),
                                                        predicateWhere, predicateInclude);

                paginacao.Values = _mapper.Map<List<FaltasEncomendasViewDto>>(paginacaoEstado.Values);

                paginacao.PageCount = paginacaoEstado.PageCount;

                return paginacao;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private Expression<Func<FaltasEncomendas, string>> GetExpression(string sortBy)
        {
            switch (sortBy)
            {
                case "Status":
                    return x => x.Status.ToString();
                case "Produto":
                    return x => x.Produto.Descricao;
                case "Vendedor":
                    return x => x.Vendedor.Nome;
                case "Tipo":
                    return x => x.Tipo.ToString();
                default:
                    return x => x.DataDeCadastro.ToString();
            }
        }
    }
}