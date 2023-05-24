using Microsoft.AspNetCore.Mvc;
using Ms_Compras.Database.Entidades;
using Ms_Compras.Dtos.NotaFiscalDeEntrada;
using Ms_Compras.Dtos;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Ms_Compras.Database.Interfaces;
using Ms_Compras.Services.Interfaces;

namespace Ms_Compras.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class FiltroNotaFiscalEntradaController : ControllerBase
    {
        private readonly INotaFiscalDeEntradaService _notaFiscalDeEntradaService;
        private readonly IGenericRepository<ErrorLogger> _genericRepository;
        private readonly IPermissaoService _permissaoService;
        private string? _empresaId;
        private string? _nivelUsuario;

        public FiltroNotaFiscalEntradaController(IGenericRepository<ErrorLogger> genericRepository,
            IPermissaoService permissaoService,
            INotaFiscalDeEntradaService notaFiscalDeEntradaService
            )
        {
            _notaFiscalDeEntradaService = notaFiscalDeEntradaService;
            _permissaoService = permissaoService;
            _genericRepository = genericRepository;
        }

        [HttpPost("/api/ListaUltimasCompras")]
        public async Task<IActionResult> ListaUltimasCompras([FromBody] FiltroUltimasCompras filtroUltimasCompras)
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
                if (!await _permissaoService.ValidarPermissaoUsuario("9fc0068f-784c-4c91-a922-a6f0e2bfeae2", token)) return NotFound();
            }

            try
            {
                _empresaId = identity?.Claims.FirstOrDefault(c => c.Type == "EmpresaId")?.Value;
                var result = await _notaFiscalDeEntradaService.GetUltimasComprasPorPeriodo(filtroUltimasCompras, Guid.Parse(_empresaId));

                return Ok(result);

            }
            catch (Exception ex)
            {
                var error = new ErrorLogger(Guid.Parse(_empresaId), ex.Message, "ERRAPI07", "Erro ao listar as ultimas compras.");
                await _genericRepository.AdicionarAsync(error);
                return BadRequest(new { code = "ERRAPI07" });
            }
        }
    }
}
