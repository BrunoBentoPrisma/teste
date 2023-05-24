using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ms_Compras.Database.Entidades;
using Ms_Compras.Database.Interfaces;
using Ms_Compras.Dtos;
using Ms_Compras.Dtos.CompraFornecedor;
using Ms_Compras.Services.Interfaces;
using System.Security.Claims;

namespace Ms_Compras.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class CompraFornecedorController : Controller
    {
        private readonly ICompraFornecedorService _compraFornecedorService;
        private readonly IGenericRepository<ErrorLogger> _genericRepository;
        private readonly IPermissaoService _permissaoService;
        private string? _empresaId;
        private string? _nomeUsuario;
        private string? _nivelUsuario;
        private DadosToken _dadosToken;

        public CompraFornecedorController(ICompraFornecedorService compraFornecedorService,
            IGenericRepository<ErrorLogger> genericRepository,
            IPermissaoService permissaoService
            )
        {
            _permissaoService = permissaoService;
            _genericRepository = genericRepository;
            _compraFornecedorService = compraFornecedorService;
        }

        [HttpPost("/api/AdicionarCompraFornecedor")]
        public async Task<IActionResult> AdicionarCompraFornecedor([FromBody] CompraFornecedorCreateDto compraFornecedorCreateDto)
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

                await _compraFornecedorService.AdicionarCompraFornecedorAsync(compraFornecedorCreateDto, _dadosToken);

                return Ok();
            }
            catch (Exception ex)
            {
                var error = new ErrorLogger(Guid.Parse(_empresaId), ex.Message, "ERRAPI03", "Erro ao adicionar uma nova compra fornecedor.");
                await _genericRepository.AdicionarAsync(error);
                return BadRequest(new { code = "ERRAPI03" });
            }
        }

        [HttpGet("/api/RetornaCompraFornecedorPorIdCompra")]
        public async Task<IActionResult> RetornaCompraFornecedorPorIdCompra([FromQuery] Guid id)
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
                var result = await _compraFornecedorService.GetListByCompraViewIdAsync(id, Guid.Parse(_empresaId));

                return Ok(result);
            }
            catch (Exception ex)
            {
                var error = new ErrorLogger(Guid.Parse(_empresaId), ex.Message, "ERRAPI06", "Erro ao retornar a compra.");
                await _genericRepository.AdicionarAsync(error);
                return BadRequest(new { code = "ERRAPI06" });
            }
        }
        [HttpGet("/api/RetornaCompraFornecedorPorId")]
        public async Task<IActionResult> RetornaCompraFornecedorPorId([FromQuery] Guid id)
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
                var result = await _compraFornecedorService.GetByIdAsync(id, Guid.Parse(_empresaId));

                return Ok(result);
            }
            catch (Exception ex)
            {
                var error = new ErrorLogger(Guid.Parse(_empresaId), ex.Message, "ERRAPI06", "Erro ao retornar a compra por id.");
                await _genericRepository.AdicionarAsync(error);
                return BadRequest(new { code = "ERRAPI06" });
            }
        }
        [HttpDelete("/api/ExcluirCompraFornecedor")]
        public async Task<IActionResult> ExcluirCompraFornecedor([FromQuery] Guid id)
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
                await _compraFornecedorService.ExcluirCompraFornecedorByIdAsync(id, Guid.Parse(_empresaId));

                return Ok();
            }
            catch (Exception ex)
            {
                var error = new ErrorLogger(Guid.Parse(_empresaId), ex.Message, "ERRAPI05", "Erro ao excluir a compra fornecedor.");
                await _genericRepository.AdicionarAsync(error);
                return BadRequest(new { code = "ERRAPI05" });
            }
        }

        [HttpPut("/api/EditarStatusCompraFornecedor")]
        public async Task<IActionResult> EditarStatusCompraFornecedor([FromBody] EditarStatusDto editarStatusDto)
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
                if(!await _permissaoService.ValidarPermissaoUsuario("01972d50-bb7e-4b57-883a-70bd379502cf", token)) return NotFound();
            }

            

            try
            {
                _empresaId = identity?.Claims.FirstOrDefault(c => c.Type == "EmpresaId")?.Value;
                _nomeUsuario = identity?.Claims?.FirstOrDefault(c => c.Type == "Nome")?.Value;
                _dadosToken = new DadosToken(Guid.Parse(_empresaId), _nomeUsuario);
                var result = await _compraFornecedorService.EditarStatusCompraFornecedor(editarStatusDto, _dadosToken);

                if(!result) return BadRequest(new { code = "ERRAPI04" });

                return Ok();
            }
            catch (Exception ex)
            {
                var error = new ErrorLogger(Guid.Parse(_empresaId), ex.Message, "ERRAPI04", $"Erro ao editar o status do pedido. {editarStatusDto.Id}");
                await _genericRepository.AdicionarAsync(error);
                return BadRequest(new { code = "ERRAPI04" });
            }
        }
    }
}
