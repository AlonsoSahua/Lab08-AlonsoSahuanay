using Lab08_AlonsoSahuanay.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Lab08_AlonsoSahuanay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Ejercicio6Controller : ControllerBase
    {
        private readonly Lab08DbContext _context;

        public Ejercicio6Controller(Lab08DbContext context)
        {
            _context = context;
        }

        [HttpGet("ObtenerPedidosPosteriores")]
        public ActionResult<IEnumerable<Order>> ObtenerPedidosPosteriores(DateTime fecha)
        {
            var pedidos = _context.Orders
                .Where(o => o.OrderDate > fecha)
                .Include(o => o.Client) 
                .ToList();

            if (!pedidos.Any())
            {
                return NotFound($"No se encontraron pedidos posteriores a {fecha.ToString("yyyy-MM-dd")}");
            }

            return Ok(pedidos);
        }
    }
}