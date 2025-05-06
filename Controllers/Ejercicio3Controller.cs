using Lab08_AlonsoSahuanay.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Lab08_AlonsoSahuanay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Ejercicio3Controller : ControllerBase
    {
        private readonly Lab08DbContext _context;

        public Ejercicio3Controller(Lab08DbContext context)
        {
            _context = context;
        }

        [HttpGet("GetOrderDetails/{orderId}")]
        public ActionResult<IEnumerable<object>> GetOrderDetails(int orderId)
        {
            var orderExists = _context.Orders.Any(o => o.OrderId == orderId);
            if (!orderExists)
            {
                return NotFound($"No se encontró la orden con ID {orderId}");
            }

            var orderDetails = _context.OrderDetails
                .Where(od => od.OrderId == orderId)
                .Include(od => od.Product) 
                .Select(od => new
                {
                    ProductName = od.Product.Name,
                    od.Quantity,
                    UnitPrice = od.Product.Price,
                    TotalPrice = od.Quantity * od.Product.Price
                })
                .ToList();

            return Ok(orderDetails);
        }
    }
}