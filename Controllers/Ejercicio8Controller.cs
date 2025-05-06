using Lab08_AlonsoSahuanay.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Lab08_AlonsoSahuanay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Ejercicio8Controller : ControllerBase
    {
        private readonly Lab08DbContext _context;

        public Ejercicio8Controller(Lab08DbContext context)
        {
            _context = context;
        }

        [HttpGet("ObtenerProductosSinDescripcion")]
        public ActionResult<IEnumerable<Product>> ObtenerProductosSinDescripcion()
        {
            var productos = _context.Products
                .Where(p => string.IsNullOrEmpty(p.Description)) 
                .ToList();

            if (!productos.Any())
            {
                return Ok(new { 
                    Mensaje = "Todos los productos tienen descripción registrada",
                    Cantidad = 0 
                });
            }

            return Ok(new {
                Cantidad = productos.Count,
                Productos = productos,
                Mensaje = $"Se encontraron {productos.Count} productos sin descripción"
            });
        }
    }
}