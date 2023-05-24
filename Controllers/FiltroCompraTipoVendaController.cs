using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ms_Compras.Database.Entidades;
using Ms_Compras.Database.Interfaces;
using Ms_Compras.Dtos.Compra;
using Ms_Compras.Services.Interfaces;
using System.Security.Claims;

namespace Ms_Compras.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class FiltroCompraTipoVendaController : ControllerBase
    {
        private readonly IFiltroCompraTipoVendaService _filtroVendaService;
        private readonly IGenericRepository<ErrorLogger> _genericRepository;
        private readonly IPermissaoService _permissaoService;
        private string? _empresaId;
        private string? _nivelUsuario;

        public FiltroCompraTipoVendaController(IFiltroCompraTipoVendaService filtroVendaService,
            IGenericRepository<ErrorLogger> genericRepository,
            IPermissaoService permissaoService
            )
        {
            _permissaoService = permissaoService;
            _genericRepository = genericRepository;
            _filtroVendaService = filtroVendaService;
        }

        [HttpPost("/api/GetFiltroComprasTipoVenda")]
        public async Task<IActionResult> GetFiltroComprasTipoVenda([FromBody] FiltroCompraDto filtroCompraDto)
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
                filtroCompraDto.EmpresaId = Guid.Parse(_empresaId);

                var result = await _filtroVendaService.GetFiltroItensCompra(filtroCompraDto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                var error = new ErrorLogger(Guid.Parse(_empresaId), ex.Message, "ERRAPI07", "Erro ao listar sugestões de compra tipo venda.");
                await _genericRepository.AdicionarAsync(error);
                return BadRequest(new { code = "ERRAPI03" });
            }
        }
    }
}
