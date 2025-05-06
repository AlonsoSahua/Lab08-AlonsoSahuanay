using Lab08_AlonsoSahuanay.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Lab08_AlonsoSahuanay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Ejercicio12Controller : ControllerBase
    {
        private readonly Lab08DbContext _context;

        public Ejercicio12Controller(Lab08DbContext context)
        {
            _context = context;
        }

        [HttpGet("ObtenerClientesPorProducto/{productId}")]
        public ActionResult<object> ObtenerClientesPorProducto(int productId)
        {
            var productoExiste = _context.Products.Any(p => p.ProductId == productId);
            if (!productoExiste)
            {
                return NotFound($"No se encontró el producto con ID {productId}");
            }

            var clientes = _context.OrderDetails
                .Where(od => od.ProductId == productId)
                .Select(od => new
                {
                    od.Order.Client.ClientId,
                    od.Order.Client.Name,
                    od.Order.Client.Email,
                    FechaCompra = od.Order.OrderDate.ToString("yyyy-MM-dd"),
                    od.Quantity,
                    TotalGastado = od.Quantity * od.Product.Price
                })
                .Distinct() 
                .ToList();

            if (!clientes.Any())
            {
                var producto = _context.Products.Find(productId);
                return Ok(new
                {
                    Mensaje = $"El producto '{producto.Name}' no ha sido comprado por ningún cliente",
                    Producto = producto.Name
                });
            }

            return Ok(new
            {
                Producto = _context.Products.Find(productId).Name,
                TotalClientes = clientes.Count,
                TotalUnidadesVendidas = clientes.Sum(c => c.Quantity),
                Clientes = clientes
            });
        }
    }
}