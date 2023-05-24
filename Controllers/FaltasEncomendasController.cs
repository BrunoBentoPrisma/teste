using Microsoft.AspNetCore.Mvc;
using Ms_Compras.Database.Interfaces;
using Ms_Compras.Services.Interfaces;
using Ms_Compras.Database.Entidades;
using Ms_Compras.Dtos;
using Ms_Compras.Dtos.FaltasEncomendas;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Ms_Compras.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class FaltasEncomendasController : ControllerBase
    {
        private readonly IFaltasEncomendasService _faltasEncomendasService;
        private readonly IGenericRepository<ErrorLogger> _genericRepository;
        private readonly IPermissaoService _permissaoService;
        private string? _empresaId;
        private string? _nomeUsuario;
        private string? _nivelUsuario;
        private DadosToken _dadosToken;

        public FaltasEncomendasController(IFaltasEncomendasService faltasEncomendasService,
            IGenericRepository<ErrorLogger> genericRepository,
            IPermissaoService permissaoService
            )
        {
            _permissaoService = permissaoService;
            _genericRepository = genericRepository;
            _faltasEncomendasService = faltasEncomendasService;
        }

        [HttpPost("/api/AdicionarFaltasEncomendas")]
        public async Task<IActionResult> AdicionarFaltasEncomendas([FromBody] List<FaltasEncomendasCreateDto> faltasEncomendasAddDtos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { code = "ERRAPI03" });
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;

            _nivelUsuario = identity?.Claims?.FirstOrDefault(c => c.Type == "Nivel")?.Value;
            
            if(int.Parse(_nivelUsuario) == 0)
            {
                var token = Response.HttpContext.Request.Headers["Authorization"];
                if(!await _permissaoService.ValidarPermissaoUsuario("01443f23-c3e4-45de-beea-2a88e92cdb40", token)) return NotFound();
            }

            try
            {
                _empresaId = identity?.Claims.FirstOrDefault(c => c.Type == "EmpresaId")?.Value;
                _nomeUsuario = identity?.Claims?.FirstOrDefault(c => c.Type == "Nome")?.Value;
                _dadosToken = new DadosToken(Guid.Parse(_empresaId), _nomeUsuario);
                var result = _faltasEncomendasService.AdicionarFaltasEncomendasAsync(faltasEncomendasAddDtos, _dadosToken);

                return Ok();
            }
            catch (Exception ex)
            {
                var error = new ErrorLogger(Guid.Parse(_empresaId), ex.Message, "ERRAPI03", "Erro ao adicionar uma nova falta encomenda.");
                await _genericRepository.AdicionarAsync(error);
                return BadRequest(new { code = "ERRAPI03" });
            }
        }
        
        [HttpPut("/api/EditarFaltasEncomendas")]
        public async Task<IActionResult> EditarFaltasEncomendas([FromBody] FaltasEncomendasEditDto faltasEncomendasEditDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { code = "ERRAPI04" });
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;

            _nivelUsuario = identity?.Claims?.FirstOrDefault(c => c.Type == "Nivel")?.Value;
            
            if(int.Parse(_nivelUsuario) == 0)
            {
                var token = Response.HttpContext.Request.Headers["Authorization"];
                if(!await _permissaoService.ValidarPermissaoUsuario("ce8c5b9e-6226-45a7-a7cc-b406e6f40a35", token)) return NotFound();
            }

            try
            {
                _empresaId = identity?.Claims.FirstOrDefault(c => c.Type == "EmpresaId")?.Value;
                _nomeUsuario = identity?.Claims?.FirstOrDefault(c => c.Type == "Nome")?.Value;
                _dadosToken = new DadosToken(Guid.Parse(_empresaId), _nomeUsuario);
                var result = await _faltasEncomendasService.EditarFaltasEncomendasAsync(faltasEncomendasEditDto, _dadosToken);

                if (!result) BadRequest(new { code = "ERRAPI04" });

                return Ok();
            }
            catch (Exception ex)
            {
                var error = new ErrorLogger(Guid.Parse(_empresaId), ex.Message, "ERRAPI04", "Erro ao editar a falta encomenda.");
                await _genericRepository.AdicionarAsync(error);
                return BadRequest(new { code = "ERRAPI04" });
            }
        }

        [HttpDelete("/api/ExcluirFaltasEncomendas")]
        public async Task<IActionResult> ExcluirFaltasEncomendas([FromQuery] Guid id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            _nivelUsuario = identity?.Claims?.FirstOrDefault(c => c.Type == "Nivel")?.Value;
            
            if(int.Parse(_nivelUsuario) == 0)
            {
                var token = Response.HttpContext.Request.Headers["Authorization"];
                if(!await _permissaoService.ValidarPermissaoUsuario("9975a983-85fc-49b3-ad62-f578b3b36112", token)) return NotFound();
            }

            try
            {
                _empresaId = identity?.Claims.FirstOrDefault(c => c.Type == "EmpresaId")?.Value;
                _nomeUsuario = identity?.Claims?.FirstOrDefault(c => c.Type == "Nome")?.Value;
                _dadosToken = new DadosToken(Guid.Parse(_empresaId), _nomeUsuario);
                var result = await _faltasEncomendasService.ExcluirFaltasEncomendasAsync(id, _dadosToken);

                return Ok();
            }
            catch (Exception ex)
            {
                var error = new ErrorLogger(Guid.Parse(_empresaId), ex.Message, "ERRAPI05", "Erro ao excluir a falta encomenda.");
                await _genericRepository.AdicionarAsync(error);
                return BadRequest(new { code = "ERRAPI05" });
            }
        }
        
        [HttpGet("/api/RetornaFaltasEncomendasPorId")]
        public async Task<IActionResult> RetornaFaltasEncomendasPorId([FromQuery] Guid id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            _nivelUsuario = identity?.Claims?.FirstOrDefault(c => c.Type == "Nivel")?.Value;

            if(int.Parse(_nivelUsuario) == 0)
            {
                var token = Response.HttpContext.Request.Headers["Authorization"];
                if(!await _permissaoService.ValidarPermissaoUsuario("34152fd7-f5cb-47e4-ba74-bb4a49cebbd9", token)) return NotFound();
            }

            try
            {
                _empresaId = identity?.Claims.FirstOrDefault(c => c.Type == "EmpresaId")?.Value;

                var result = await _faltasEncomendasService.GetByIdAsync(id, Guid.Parse(_empresaId));

                return Ok(result);
            }
            catch (Exception ex)
            {
                var error = new ErrorLogger(Guid.Parse(_empresaId), ex.Message, "ERRAPI06", "Erro ao retornar a falta encomenda.");
                await _genericRepository.AdicionarAsync(error);
                return BadRequest(new { code = "ERRAPI06" });
            }
        }
        
        [HttpGet("/api/ListaPaginacaoFaltasEncomendas")]
        public async Task<IActionResult> ListaPaginacaoFaltasEncomendas(
            [FromQuery] int page,
            [FromQuery] int pageSize,
            [FromQuery] bool asc,
            [FromQuery] string sortBy = "",
            [FromQuery] string search = ""
            )
        {
            
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            _nivelUsuario = identity?.Claims?.FirstOrDefault(c => c.Type == "Nivel")?.Value;

            if(int.Parse(_nivelUsuario) == 0)
            {
                var token = Response.HttpContext.Request.Headers["Authorization"];
                if(!await _permissaoService.ValidarPermissaoUsuario("4828913e-7a5b-42fa-aaf1-da65165462bc", token)) return NotFound();
            }
            
            try
            {
                _empresaId = identity?.Claims.FirstOrDefault(c => c.Type == "EmpresaId")?.Value;
                var paginacaoRequestDto = new PaginacaoRequestDto(page,pageSize,search,sortBy,asc,Guid.Parse(_empresaId));

                var paginacao = await this._faltasEncomendasService.GetPaginacaoAsync(paginacaoRequestDto);

                return Ok(paginacao);
            }
            catch (Exception ex)
            {
                var error = new ErrorLogger(Guid.Parse(_empresaId), ex.Message, "ERRAPI07", "Erro ao retornar a paginação de faltas encomendas.");
                await _genericRepository.AdicionarAsync(error);
                return BadRequest(new { code = "ERRAPI07" });
            }
        }
    
        [HttpGet("/api/ListaFaltasEncomendas")]
        public async Task<IActionResult> ListaFaltasEncomendas()
        {
            
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            _nivelUsuario = identity?.Claims?.FirstOrDefault(c => c.Type == "Nivel")?.Value;
            
            if(int.Parse(_nivelUsuario) == 0)
            {
                var token = Response.HttpContext.Request.Headers["Authorization"];
                if(!await _permissaoService.ValidarPermissaoUsuario("4828913e-7a5b-42fa-aaf1-da65165462bc", token)) return NotFound();
            }


            try
            {
                _empresaId = identity?.Claims.FirstOrDefault(c => c.Type == "EmpresaId")?.Value;
                var faltasEncomendasViewDtos = await this._faltasEncomendasService.GetAllAsync(Guid.Parse(_empresaId));

                return Ok(faltasEncomendasViewDtos);
            }
            catch (Exception ex)
            {
                var error = new ErrorLogger(Guid.Parse(_empresaId), ex.Message, "ERRAPI07", "Erro ao listar as faltas encomendas.");
                await _genericRepository.AdicionarAsync(error);
                return BadRequest(new { code = "ERRAPI07" });
            }
        }
    
        [HttpGet("/api/ConcluirFaltasEncomendas")]
        public async Task<IActionResult> ConcluirFaltasEncomendas([FromQuery] Guid id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            _nivelUsuario = identity?.Claims?.FirstOrDefault(c => c.Type == "Nivel")?.Value;
            
            if(int.Parse(_nivelUsuario) == 0)
            {
                var token = Response.HttpContext.Request.Headers["Authorization"];
                if(!await _permissaoService.ValidarPermissaoUsuario("3cfd67ff-28f9-42cf-9aa9-1da70323b669", token)) return NotFound();
            }

            try
            {
                _empresaId = identity?.Claims.FirstOrDefault(c => c.Type == "EmpresaId")?.Value;
                _nomeUsuario = identity?.Claims?.FirstOrDefault(c => c.Type == "Nome")?.Value;
                _dadosToken = new DadosToken(Guid.Parse(_empresaId), _nomeUsuario);
                var result = await _faltasEncomendasService.ConcluirFaltasEncomendasAsync(id, _dadosToken);

                if(!result) return BadRequest(new { code = "ERRAPI03" });

                return Ok();
            }
            catch (Exception ex)
            {
                var error = new ErrorLogger(Guid.Parse(_empresaId), ex.Message, "ERRAPI03", "Erro ao adicionar uma nova falta encomenda.");
                await _genericRepository.AdicionarAsync(error);
                return BadRequest(new { code = "ERRAPI03" });
            }

        }
    }
}