using Lab08_AlonsoSahuanay.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Lab08_AlonsoSahuanay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Ejercicio7Controller : ControllerBase
    {
        private readonly Lab08DbContext _context;

        public Ejercicio7Controller(Lab08DbContext context)
        {
            _context = context;
        }

        [HttpGet("ObtenerPromedioPrecios")]
        public ActionResult<object> ObtenerPromedioPrecios()
        {
            var promedio = _context.Products
                .Average(p => p.Price);

            var conteo = _context.Products.Count();

            return Ok(new
            {
                PromedioPrecio = Math.Round(promedio, 2),
                CantidadProductos = conteo,
                Mensaje = $"El precio promedio de los {conteo} productos es: {Math.Round(promedio, 2)}"
            });
        }
    }
}