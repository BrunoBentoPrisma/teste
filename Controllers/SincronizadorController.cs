using Microsoft.AspNetCore.Mvc;
using Ms_Compras.SincronizadosDeDados;

namespace Ms_Compras.Controllers
{
    public class SincronizadorController : Controller
    {
        private readonly ISincronizador _sincronizador;
        public SincronizadorController(ISincronizador sincronizador)
        {
            _sincronizador = sincronizador;
        }

        [HttpGet("/api/SincronizarVendedor")]
        public async Task<IActionResult> SincronizarVendedor()
        {
            try
            {
                await _sincronizador.SincronizarVendedor();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/api/SincronizarGrupo")]
        public async Task<IActionResult> SincronizarGrupo()
        {
            try
            {
                await _sincronizador.SincronizarGrupo();
                
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("/api/SincronizarLaboratorio")]
        public async Task<IActionResult> SincronizarLaboratorio()
        {
            try
            {
                await _sincronizador.SincronizarLaboratorio();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("/api/SincronizarFornecedor")]
        public async Task<IActionResult> SincronizarFornecedor()
        {
            try
            {
                await _sincronizador.SincronizarFornecedor();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("/api/SincronizarProduto")]
        public async Task<IActionResult> SincronizarProduto()
        {
            try
            {
                await _sincronizador.SincronizarProduto();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("/api/SincronizarCliente")]
        public async Task<IActionResult> SincronizarCliente()
        {
            try
            {
                await _sincronizador.SincronizarClientes();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("/api/SincronizarNfeEntrada")]
        public async Task<IActionResult> SincronizarNfeEntrada()
        {
            try
            {
                await _sincronizador.SincronizarNfeEntrada();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("/api/SincronizarItemNfeEntrada")]
        public async Task<IActionResult> SincronizarItemNfeEntrada()
        {
            try
            {
                await _sincronizador.SincronizarItensNfeEntrada();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("/api/SincronizarOrdem")]
        public async Task<IActionResult> SincronizarOrdem()
        {
            try
            {
                await _sincronizador.SincronizarOrdemDeProducao();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("/api/SincronizarItemOrdem")]
        public async Task<IActionResult> SincronizarItemOrdem()
        {
            try
            {
                await _sincronizador.SincronizarItensOrdemDeProducao();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    
        [HttpGet("/api/SincronizarEmbalagem")]
        public async Task<IActionResult> SincronizarEmbalagem()
        {
            try
            {
                await _sincronizador.SincronizarEmbalagem();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/api/SincronizarItemEmbalagem")]
        public async Task<IActionResult> SincronizarItemEmbalagem()
        {
            try
            {
                await _sincronizador.SincronizarItemEmbalagem();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/api/SincronizarVenda")]
        public async Task<IActionResult> SincronizarVenda()
        {
            try
            {
                await _sincronizador.SincronizarVenda();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/api/SincronizarItemVenda")]
        public async Task<IActionResult> SincronizarItemVenda()
        {
            try
            {
                await _sincronizador.SincronizarItemVenda();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/api/SincronizarLote")]
        public async Task<IActionResult> SincronizarLote()
        {
            try
            {
                await _sincronizador.SincronizarLote();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/api/SincronizarFaltas")]
        public async Task<IActionResult> SincronizarFaltas()
        {
            try
            {
                await _sincronizador.SincronizarFaltas();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/api/SincronizarMovimento")]
        public async Task<IActionResult> SincronizarMovimento()
        {
            try
            {
                await _sincronizador.SincronizarMovimento();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/api/SincronizarItensFormula")]
        public async Task<IActionResult> SincronizarItensFormula()
        {
            try
            {
                await _sincronizador.SincronizarItensFormuzaVenda();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
