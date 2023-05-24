using Microsoft.AspNetCore.Mvc;
using Ms_Compras.Dtos;
using Ms_Compras.Services.Interfaces;
using Ms_Compras.Database.Interfaces;
using Ms_Compras.Database.Entidades;
using Ms_Compras.Dtos.CompraFornecedor;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Ms_Compras.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ItemCompraFornecedorController : ControllerBase
    {
        private readonly IItemCompraFornecedorService _itemCompraFornecedorService;
        private readonly IGenericRepository<ErrorLogger> _genericRepository;
        private readonly IPermissaoService _permissaoService;
        private string? _empresaId;
        private string? _nomeUsuario;
        private string? _nivelUsuario;
        private DadosToken _dadosToken;

        public ItemCompraFornecedorController(IItemCompraFornecedorService itemCompraFornecedorService,
            IGenericRepository<ErrorLogger> genericRepository,
            IPermissaoService permissaoService
            )
        {
            _permissaoService = permissaoService;
            _genericRepository = genericRepository;
            _itemCompraFornecedorService = itemCompraFornecedorService;
        }


        [HttpPut("/api/EditarStatusItensCompraFornecedor")]
        public async Task<IActionResult> EditarStatusItensCompraFornecedor([FromBody] EditarStatusItemDto editarStatusItemDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { code = "ERRAPI06" });
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;

            _nivelUsuario = identity?.Claims?.FirstOrDefault(c => c.Type == "Nivel")?.Value;

            if (int.Parse(_nivelUsuario) == 0)
            {
                var token = Response.HttpContext.Request.Headers["Authorization"];
                if (!await _permissaoService.ValidarPermissaoUsuario("01972d50-bb7e-4b57-883a-70bd379502cf", token)) return NotFound();
            }

            try
            {
                _empresaId = identity?.Claims.FirstOrDefault(c => c.Type == "EmpresaId")?.Value;
                _nomeUsuario = identity?.Claims?.FirstOrDefault(c => c.Type == "Nome")?.Value;
                _dadosToken = new DadosToken(Guid.Parse(_empresaId), _nomeUsuario);
                var result = await _itemCompraFornecedorService.EditarStatusItem(editarStatusItemDto, _dadosToken);

                if(!result) return BadRequest(new { code = "ERRAPI06" });

                return Ok();
            }
            catch (Exception ex)
            {
                var error = new ErrorLogger(Guid.Parse(_empresaId), ex.Message, "ERRAPI03", "Erro ao retornar a consulta do pedido.");
                await _genericRepository.AdicionarAsync(error);
                return BadRequest(new { code = "ERRAPI06" });
            }
        }
        
    }
}