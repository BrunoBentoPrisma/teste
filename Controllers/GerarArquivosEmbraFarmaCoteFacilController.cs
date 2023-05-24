using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ms_Compras.Database.Interfaces;
using Ms_Compras.Database.Entidades;
using Ms_Compras.Services.Interfaces;
using Ms_Compras.Dtos;
using System.Security.Claims;

namespace Ms_Compras.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class GerarArquivosEmbraFarmaCoteFacilController : ControllerBase
    {
        private readonly IGerarArquivoCoteFacilService _gerarArquivoCoteFacilService;
        private readonly IGerarArquivosEmbraFarmaService _gerarArquivosEmbraFarmaService;
        private readonly IGenericRepository<ErrorLogger> _genericRepository;
        private readonly IPermissaoService _permissaoService;
        private string? _empresaId;
        private string? _nivelUsuario;
        private string? _nomeUsuario;
        private DadosToken _dadosToken;

        public GerarArquivosEmbraFarmaCoteFacilController(IGerarArquivoCoteFacilService gerarArquivoCoteFacilService,
            IGenericRepository<ErrorLogger> genericRepository,
            IPermissaoService permissaoService,
            IGerarArquivosEmbraFarmaService gerarArquivosEmbraFarmaService
            )
        {
            _gerarArquivoCoteFacilService = gerarArquivoCoteFacilService;
            _gerarArquivosEmbraFarmaService = gerarArquivosEmbraFarmaService;
            _permissaoService = permissaoService;
            _genericRepository = genericRepository;
        }

        [HttpPost("/api/ExportCotacaoCompraCoteFacil")]
        public async Task<IActionResult> ExportCotacaoCompraCoteFacil([FromBody] List<Guid> guids)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            _nivelUsuario = identity?.Claims?.FirstOrDefault(c => c.Type == "Nivel")?.Value;
            _empresaId = identity?.Claims.FirstOrDefault(c => c.Type == "EmpresaId")?.Value;

            if (int.Parse(_nivelUsuario) == 0)
            {
                var token = Response.HttpContext.Request.Headers["Authorization"];
                if (!await _permissaoService.ValidarPermissaoUsuario("a4d8d045-f8ed-4a9e-9ac2-1f096815af7e", token)) return NotFound();
            }


            try
            {
                _dadosToken = new DadosToken(Guid.Parse(_empresaId), _nomeUsuario);

                var result = await _gerarArquivoCoteFacilService.GerarArquivo(guids, _dadosToken);

                return Ok(result);
            }
            catch (Exception ex)
            {
                var error = new ErrorLogger(Guid.Parse(_empresaId), ex.Message, "ERRAPI06", "Erro ao exportar arquivo cote facil.");
                await _genericRepository.AdicionarAsync(error);
                return BadRequest(new { code = "ERRAPI06" });
            }
        }

        [HttpPost("/api/ExportCotacaoCompraEmbraFarma")]
        public async Task<IActionResult> ExportCotacaoCompraEmbraFarma([FromBody] CotacaoCompraEmbraFarmaDto cotacaoCompraEmbraFarmaDto)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            _nivelUsuario = identity?.Claims?.FirstOrDefault(c => c.Type == "Nivel")?.Value;
            _empresaId = identity?.Claims.FirstOrDefault(c => c.Type == "EmpresaId")?.Value;

            if (int.Parse(_nivelUsuario) == 0)
            {
                var token = Response.HttpContext.Request.Headers["Authorization"];
                if (!await _permissaoService.ValidarPermissaoUsuario("a4d8d045-f8ed-4a9e-9ac2-1f096815af7e", token)) return NotFound();
            }


            try
            {
                _dadosToken = new DadosToken(Guid.Parse(_empresaId), _nomeUsuario);

                var result = await _gerarArquivosEmbraFarmaService.GerarArquivo(cotacaoCompraEmbraFarmaDto, _dadosToken);

                return Ok(result);
            }
            catch (Exception ex)
            {
                var error = new ErrorLogger(Guid.Parse(_empresaId), ex.Message, "ERRAPI06", "Erro ao exportar arquivo embra farma.");
                await _genericRepository.AdicionarAsync(error);
                return BadRequest(new { code = "ERRAPI06" });
            }
        }
    }
}