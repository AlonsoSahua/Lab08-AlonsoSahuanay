using Lab08_AlonsoSahuanay.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Lab08_AlonsoSahuanay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Ejercicio10Controller : ControllerBase
    {
        private readonly Lab08DbContext _context;

        public Ejercicio10Controller(Lab08DbContext context)
        {
            _context = context;
        }

        [HttpGet("ObtenerPedidosConDetalles")]
        public ActionResult<IEnumerable<object>> ObtenerPedidosConDetalles()
        {
            var pedidosConDetalles = _context.Orders
                .Include(o => o.Client) 
                .Include(o => o.OrderDetails) 
                .ThenInclude(od => od.Product) 
                .Select(o => new
                {
                    PedidoId = o.OrderId,
                    Fecha = o.OrderDate.ToString("yyyy-MM-dd"),
                    Cliente = o.Client.Name,
                    EmailCliente = o.Client.Email,
                    Detalles = o.OrderDetails.Select(od => new
                    {
                        Producto = od.Product.Name,
                        od.Quantity,
                        PrecioUnitario = od.Product.Price,
                        Total = od.Quantity * od.Product.Price
                    }),
                    TotalPedido = o.OrderDetails.Sum(od => od.Quantity * od.Product.Price)
                })
                .ToList();

            if (!pedidosConDetalles.Any())
            {
                return NotFound("No se encontraron pedidos en la base de datos");
            }

            return Ok(pedidosConDetalles);
        }
    }
}