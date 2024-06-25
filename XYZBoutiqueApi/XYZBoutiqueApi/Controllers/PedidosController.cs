using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XYZBoutiqueApi.Data;
using XYZBoutiqueApi.Models;
using XYZBoutiqueApi.ViewModels;
using System;
using System.Threading.Tasks;

namespace XYZBoutiqueApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PedidosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PedidosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("crear")]
        public async Task<IActionResult> CrearPedido([FromBody] PedidoDto pedidoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nuevoPedido = new Pedido
            {
                ListaProductos = pedidoDto.ListaProductos,
                FechaPedido = DateTime.Now,
                VendedorSolicitante = pedidoDto.VendedorSolicitante,
                Repartidor = pedidoDto.Repartidor,
                Estado = EstadoPedido.PorAtender
            };

            _context.Pedidos.Add(nuevoPedido);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPedido), new { id = nuevoPedido.NroPedido }, nuevoPedido);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPedido(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);

            if (pedido == null)
            {
                return NotFound();
            }

            return Ok(pedido);
        }

        [HttpPut("{id}/cambiar-estado")]
        public async Task<IActionResult> CambiarEstado(int id, [FromBody] EstadoPedido nuevoEstado)
        {
            var pedido = await _context.Pedidos.FindAsync(id);

            if (pedido == null)
            {
                return NotFound();
            }

            if (!PuedeCambiarEstado(pedido.Estado, nuevoEstado))
            {
                return BadRequest("Transición de estado no permitida.");
            }

            switch (nuevoEstado)
            {
                case EstadoPedido.EnProceso:
                    pedido.FechaRecepcion = DateTime.Now;
                    break;
                case EstadoPedido.EnDelivery:
                    pedido.FechaDespacho = DateTime.Now;
                    break;
                case EstadoPedido.Recibido:
                    pedido.FechaEntrega = DateTime.Now;
                    break;
                default:
                    return BadRequest("Estado no válido.");
            }

            pedido.Estado = nuevoEstado;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PuedeCambiarEstado(EstadoPedido estadoActual, EstadoPedido nuevoEstado)
        {
            if (estadoActual == EstadoPedido.PorAtender &&
                (nuevoEstado == EstadoPedido.EnProceso || nuevoEstado == EstadoPedido.EnDelivery || nuevoEstado == EstadoPedido.Recibido))
            {
                return true;
            }

            if (estadoActual == EstadoPedido.EnProceso &&
                (nuevoEstado == EstadoPedido.EnDelivery || nuevoEstado == EstadoPedido.Recibido))
            {
                return true;
            }

            if (estadoActual == EstadoPedido.EnDelivery && nuevoEstado == EstadoPedido.Recibido)
            {
                return true;
            }

            return false;
        }
    }
}
