using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ms_Compras.Database.Entidades;
using Ms_Compras.Database.Interfaces;
using Ms_Compras.Dtos;
using Ms_Compras.Dtos.Compra;
using Ms_Compras.Services.Interfaces;
using System.Security.Claims;

namespace Ms_Compras.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class CompraController : Controller
    {
        private readonly ICompraService _compraService;
        private readonly IGenericRepository<ErrorLogger> _genericRepository;
        private readonly IPermissaoService _permissaoService;
        private string? _empresaId;
        private string? _nivelUsuario;
        private string? _nomeUsuario;
        private DadosToken _dadosToken;

        public CompraController(ICompraService compraService,
            IGenericRepository<ErrorLogger> genericRepository,
            IPermissaoService permissaoService
            )
        {
            _permissaoService = permissaoService;
            _genericRepository = genericRepository;
            _compraService = compraService;
        }

        [HttpPost("/api/AdicionarCompra")]
        public async Task<IActionResult> AdicionarCompra([FromBody] CompraCreateDto compraCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { code = "ERRAPI03" });
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            _nivelUsuario = identity?.Claims?.FirstOrDefault(c => c.Type == "Nivel")?.Value;

            if (int.Parse(_nivelUsuario) == 0)
            {
                var token = Response.HttpContext.Request.Headers["Authorization"];
                if (!await _permissaoService.ValidarPermissaoUsuario("a65b7539-e56b-4281-bd62-dff5a66dc6a0", token)) return NotFound();
            }

            try
            {
                _empresaId = identity?.Claims.FirstOrDefault(c => c.Type == "EmpresaId")?.Value;
                _nomeUsuario = identity?.Claims?.FirstOrDefault(c => c.Type == "Nome")?.Value;
                _dadosToken = new DadosToken(Guid.Parse(_empresaId), _nomeUsuario);
                var result = await _compraService.AdicionarCompraAsync(compraCreateDto, _dadosToken);

                return Ok();
            }
            catch (Exception ex)
            {
                var error = new ErrorLogger(Guid.Parse(_empresaId), ex.Message, "ERRAPI03", "Erro ao adicionar uma nova compra.");
                await _genericRepository.AdicionarAsync(error);
                return BadRequest(new { code = "ERRAPI03" });
            }
        }
        [HttpDelete("/api/ExcluirCompra")]
        public async Task<IActionResult> ExcluirCompra([FromQuery] Guid id)
        {

            var identity = HttpContext.User.Identity as ClaimsIdentity;

            _nivelUsuario = identity?.Claims?.FirstOrDefault(c => c.Type == "Nivel")?.Value;

            if (int.Parse(_nivelUsuario) == 0)
            {
                var token = Response.HttpContext.Request.Headers["Authorization"];
                if (!await _permissaoService.ValidarPermissaoUsuario("4cbc7b05-3716-4cd4-bc22-c70624206aff", token)) return NotFound();
            }

            try
            {
                _empresaId = identity?.Claims.FirstOrDefault(c => c.Type == "EmpresaId")?.Value;
                _nomeUsuario = identity?.Claims?.FirstOrDefault(c => c.Type == "Nome")?.Value;
                _dadosToken = new DadosToken(Guid.Parse(_empresaId), _nomeUsuario);
                var result = await this._compraService.ExcluirCompraAsync(id, _dadosToken);

                return Ok();
            }
            catch (Exception ex)
            {
                var error = new ErrorLogger(Guid.Parse(_empresaId), ex.Message, "ERRAPI05", "Erro ao excluir a compra.");
                await _genericRepository.AdicionarAsync(error);
                return BadRequest(new { code = "ERRAPI05" });
            }
        }
        [HttpGet("/api/RetornaCompraPorId")]
        public async Task<IActionResult> RetornaCompraPorId([FromQuery] Guid id)
        {

            var identity = HttpContext.User.Identity as ClaimsIdentity;

            _nivelUsuario = identity?.Claims?.FirstOrDefault(c => c.Type == "Nivel")?.Value;

            if (int.Parse(_nivelUsuario) == 0)
            {
                var token = Response.HttpContext.Request.Headers["Authorization"];
                if (!await _permissaoService.ValidarPermissaoUsuario("d7050403-df8e-4c49-b43a-e0eeece3c247", token)) return NotFound();
            }


            try
            {
                _empresaId = identity?.Claims.FirstOrDefault(c => c.Type == "EmpresaId")?.Value;
                var result = await _compraService.GetByIdAsync(id, Guid.Parse(_empresaId));

                return Ok(result);
            }
            catch (Exception ex)
            {
                var error = new ErrorLogger(Guid.Parse(_empresaId), ex.Message, "ERRAPI06", "Erro ao retornar a compra.");
                await _genericRepository.AdicionarAsync(error);
                return BadRequest(new { code = "ERRAPI06" });
            }
        }
        [HttpGet("/api/ListaCompra")]
        public async Task<IActionResult> ListaCompra()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var token = Response.HttpContext.Request.Headers["Authorization"];
            _nivelUsuario = identity?.Claims?.FirstOrDefault(c => c.Type == "Nivel")?.Value;
            _empresaId = identity?.Claims.FirstOrDefault(c => c.Type == "EmpresaId")?.Value;
            _nomeUsuario = identity?.Claims?.FirstOrDefault(c => c.Type == "Nome")?.Value;

            if (int.Parse(_nivelUsuario) == 0)
                if (!await _permissaoService.ValidarPermissaoUsuario("bbe02ce9-6555-4833-9ff8-accea76d7455", token)) return NotFound();


            try
            {
                var fornecedores = await this._compraService.GetAllAsync(Guid.Parse(_empresaId));

                return Ok(fornecedores);
            }
            catch (Exception ex)
            {
                var error = new ErrorLogger(Guid.Parse(_empresaId), ex.Message, "ERRAPI07", "Erro ao listar as compras.");
                await _genericRepository.AdicionarAsync(error);
                return BadRequest(new { code = "ERRAPI07" });
            }
        }
        [HttpGet("/api/ListaPaginacaoCompra")]
        public async Task<IActionResult> ListaPaginacaoCompra(
            [FromQuery] int page,
            [FromQuery] int pageSize,
            [FromQuery] bool asc,
            [FromQuery] string sortBy = "",
            [FromQuery] string search = ""
            )
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            _nivelUsuario = identity?.Claims?.FirstOrDefault(c => c.Type == "Nivel")?.Value;

            if (int.Parse(_nivelUsuario) == 0)
            {
                var token = Response.HttpContext.Request.Headers["Authorization"];
                if (!await _permissaoService.ValidarPermissaoUsuario("bbe02ce9-6555-4833-9ff8-accea76d7455", token)) return NotFound();
            }

            try
            {
                _empresaId = identity?.Claims.FirstOrDefault(c => c.Type == "EmpresaId")?.Value;

                var paginacaoRequestDto = new PaginacaoRequestDto(page,pageSize,search,sortBy,asc,Guid.Parse(_empresaId));

                var paginacao = await this._compraService.GetPaginacaoAsync(paginacaoRequestDto);

                return Ok(paginacao);
            }
            catch (Exception ex)
            {
                var error = new ErrorLogger(Guid.Parse(_empresaId), ex.Message, "ERRAPI07", "Erro ao retornar a paginação de fornecedores.");
                await _genericRepository.AdicionarAsync(error);
                return BadRequest(new { code = "ERRAPI07" });
            }
        }
    }
}
