using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ms_Compras.Database.Interfaces;
using System.Security.Claims;

namespace Ms_Compras.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ClienteEvendedorController : ControllerBase
    {
        /// <summary>
        ///   Excluir este controller assim que for criado o Ms-Vendas e adicionar 
        ///   as entidades Cliente e Vendedor no Ms-Vendas
        /// </summary>
        private readonly IClienteVendedorRepository _clienteVendedorRepository;
        private string? _empresaId;

        public ClienteEvendedorController(IClienteVendedorRepository clienteVendedorRepository)
        {
            _clienteVendedorRepository = clienteVendedorRepository;
        }

        [HttpGet("/api/ListaCliente")]
        public async Task<IActionResult> ListaCliente()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                _empresaId = identity?.Claims.FirstOrDefault(c => c.Type == "EmpresaId")?.Value;

                var clientes = await _clienteVendedorRepository.GetClientes(Guid.Parse(_empresaId));

                return Ok(clientes);
            
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);   
            }

        }

        [HttpGet("/api/ListaVendedor")]
        public async Task<IActionResult> ListaVendedor()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                _empresaId = identity?.Claims.FirstOrDefault(c => c.Type == "EmpresaId")?.Value;

                var vendedores = await _clienteVendedorRepository.GetVendedores(Guid.Parse(_empresaId));

                return Ok(vendedores);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
