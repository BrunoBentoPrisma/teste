using Microsoft.AspNetCore.Mvc;
using Ms_Compras.Database.Entidades;
using Ms_Compras.Dtos.CompraFornecedor;
using System.Security.Claims;
using Ms_Compras.Services.Interfaces;
using Ms_Compras.Database.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Ms_Compras.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class FiltroCompraFornecedorController : ControllerBase
    {
        private readonly IFiltroCompraFornecedorService _filtroCompraFornecedorService;
        private readonly IGenericRepository<ErrorLogger> _genericRepository;
        private readonly IPermissaoService _permissaoService;
        private string? _empresaId;
        private string? _nivelUsuario;

        public FiltroCompraFornecedorController(IFiltroCompraFornecedorService filtroCompraFornecedorService,
            IGenericRepository<ErrorLogger> genericRepository,
            IPermissaoService permissaoService
            )
        {
            _permissaoService = permissaoService;
            _genericRepository = genericRepository;
            _filtroCompraFornecedorService = filtroCompraFornecedorService;
        }

        [HttpPost("/api/Compra/FiltroCotacaoCompra")]
        public async Task<IActionResult> FiltroCotacaoCompra([FromBody] FiltroCotacaoCompraDto filtroCotacaoCompraDto)
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
                if (!await _permissaoService.ValidarPermissaoUsuario("a4d8d045-f8ed-4a9e-9ac2-1f096815af7e", token)) return NotFound();
            }

            try
            {
                _empresaId = identity?.Claims.FirstOrDefault(c => c.Type == "EmpresaId")?.Value;
                var result = await _filtroCompraFornecedorService.GetFiltroCotacaoCompra(filtroCotacaoCompraDto, Guid.Parse(_empresaId));

                return Ok(result);

            }
            catch (Exception ex)
            {
                var error = new ErrorLogger(Guid.Parse(_empresaId), ex.Message, "ERRAPI07", "Erro ao listar as cotações.");
                await _genericRepository.AdicionarAsync(error);
                return BadRequest(new { code = "ERRAPI07" });
            }
        }

        [HttpPost("/api/ConsultarCompraFornecedor")]
        public async Task<IActionResult> ConsultarCompraFornecedor([FromBody] FiltroPedidoCompraFornecedor filtroPedido)
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
                if (!await _permissaoService.ValidarPermissaoUsuario("5b7c70a1-30c9-40c7-b0ff-5caf45cdef47", token)) return NotFound();
            }

            try
            {
                _empresaId = identity?.Claims.FirstOrDefault(c => c.Type == "EmpresaId")?.Value;
                var result = await _filtroCompraFornecedorService.ConsultarPedidoCompraFornecedor(filtroPedido, Guid.Parse(_empresaId));

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
