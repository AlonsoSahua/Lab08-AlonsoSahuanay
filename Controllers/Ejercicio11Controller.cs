using Lab08_AlonsoSahuanay.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Lab08_AlonsoSahuanay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Ejercicio11Controller : ControllerBase
    {
        private readonly Lab08DbContext _context;

        public Ejercicio11Controller(Lab08DbContext context)
        {
            _context = context;
        }

        [HttpGet("ObtenerProductosPorCliente/{clientId}")]
        public ActionResult<object> ObtenerProductosPorCliente(int clientId)
        {
            var clienteExiste = _context.Clients.Any(c => c.ClientId == clientId);
            if (!clienteExiste)
            {
                return NotFound($"No se encontró el cliente con ID {clientId}");
            }

            var productosPorCliente = _context.Orders
                .Where(o => o.ClientId == clientId)
                .SelectMany(o => o.OrderDetails) 
                .Select(od => new
                {
                    od.Product.ProductId,
                    od.Product.Name,
                    od.Product.Price,
                    od.Quantity,
                    FechaPedido = od.Order.OrderDate.ToString("yyyy-MM-dd"),
                    Total = od.Quantity * od.Product.Price
                })
                .ToList();

            if (!productosPorCliente.Any())
            {
                return Ok(new
                {
                    Mensaje = $"El cliente con ID {clientId} no tiene pedidos registrados",
                    Cliente = _context.Clients.Find(clientId)?.Name
                });
            }

            return Ok(new
            {
                Cliente = _context.Clients.Find(clientId).Name,
                TotalProductosDiferentes = productosPorCliente.Select(p => p.ProductId).Distinct().Count(),
                TotalUnidades = productosPorCliente.Sum(p => p.Quantity),
                GastoTotal = productosPorCliente.Sum(p => p.Total),
                Productos = productosPorCliente
            });
        }
    }
}