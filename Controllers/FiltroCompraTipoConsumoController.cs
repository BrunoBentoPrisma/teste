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
    public class FiltroCompraTipoConsumoController : ControllerBase
    {

        private readonly IFiltroCompraTipoConsumoService _filtroCompraTipoConsumoService;
        private readonly IGenericRepository<ErrorLogger> _genericRepository;
        private readonly IPermissaoService _permissaoService;
        private string? _empresaId;
        private string? _nivelUsuario;

        public FiltroCompraTipoConsumoController(IFiltroCompraTipoConsumoService filtroCompraTipoConsumoService, 
            IGenericRepository<ErrorLogger> genericRepository, 
            IPermissaoService permissaoService)
        {
            _filtroCompraTipoConsumoService = filtroCompraTipoConsumoService;
            _genericRepository = genericRepository;
            _permissaoService = permissaoService;
        }

        [HttpPost("/api/GetFiltroComprasTipoConsumo")]
        public async Task<IActionResult> GetFiltroComprasTipoConsumo([FromBody] FiltroCompraDto filtroCompraDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { code = "ERRAPI07" });
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;

            _nivelUsuario = identity?.Claims?.FirstOrDefault(c => c.Type == "Nivel")?.Value;

            if (int.Parse(_nivelUsuario) == 0)
            {
                var token = Response.HttpContext.Request.Headers["Authorization"];
                if (!await _permissaoService.ValidarPermissaoUsuario("4828913e-7a5b-42fa-aaf1-da65165462bc", token)) return NotFound();
            }


            try
            {
                _empresaId = identity?.Claims.FirstOrDefault(c => c.Type == "EmpresaId")?.Value;
                filtroCompraDto.EmpresaId = Guid.Parse(_empresaId);
                var faltasEncomendasViewDtos = await _filtroCompraTipoConsumoService.GetFiltroItensCompra(filtroCompraDto);

                return Ok(faltasEncomendasViewDtos);
            }
            catch (Exception ex)
            {
                var error = new ErrorLogger(Guid.Parse(_empresaId), ex.Message, "ERRAPI07", "Erro ao listar sugestões de compra tipo demanda.");
                await _genericRepository.AdicionarAsync(error);
                return BadRequest(new { code = "ERRAPI07" });
            }
        }
    }
}
