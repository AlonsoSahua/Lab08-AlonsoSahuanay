using Lab08_AlonsoSahuanay.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Lab08_AlonsoSahuanay.Controllers
{
    [Route("api/ejercicio4")]
    [ApiController]
    public class Ejercicio4Controller : ControllerBase
    {
        private readonly Lab08DbContext _context;

        public Ejercicio4Controller(Lab08DbContext context)
        {
            _context = context;
        }

        [HttpGet("total-productos-orden/{orderId}")] 
        public ActionResult<object> CalcularTotalProductos(int orderId)
        {
            var orderExists = _context.Orders.Any(o => o.OrderId == orderId);
            if (!orderExists)
            {
                return NotFound($"No se encontró la orden con ID {orderId}");
            }

            var totalQuantity = _context.OrderDetails
                .Where(od => od.OrderId == orderId)
                .Sum(od => od.Quantity);

            return Ok(new 
            {
                OrderId = orderId,
                TotalItems = totalQuantity,
                Message = $"La orden {orderId} contiene {totalQuantity} productos en total"
            });
        }
    }
}