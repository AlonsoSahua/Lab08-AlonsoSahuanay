using Lab08_AlonsoSahuanay.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Lab08_AlonsoSahuanay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Ejercicio9Controller : ControllerBase
    {
        private readonly Lab08DbContext _context;

        public Ejercicio9Controller(Lab08DbContext context)
        {
            _context = context;
        }

        [HttpGet("ObtenerClienteMasFrecuente")]
        public ActionResult<object> ObtenerClienteMasFrecuente()
        {
            var clienteMasFrecuente = _context.Orders
                .Include(o => o.Client)
                .GroupBy(o => o.ClientId)
                .Select(g => new
                {
                    Cliente = g.First().Client, 
                    TotalPedidos = g.Count()
                })
                .OrderByDescending(x => x.TotalPedidos)
                .FirstOrDefault();

            if (clienteMasFrecuente == null)
            {
                return NotFound("No se encontraron pedidos en la base de datos");
            }

            return Ok(new
            {
                clienteMasFrecuente.Cliente.ClientId,
                clienteMasFrecuente.Cliente.Name,
                clienteMasFrecuente.Cliente.Email,
                clienteMasFrecuente.TotalPedidos,
                Mensaje = $"{clienteMasFrecuente.Cliente.Name} es el cliente más frecuente con {clienteMasFrecuente.TotalPedidos} pedidos"
            });
        }
    }
}