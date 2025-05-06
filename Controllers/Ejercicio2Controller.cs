using Lab08_AlonsoSahuanay.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Lab08_AlonsoSahuanay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Ejercicio2Controller : ControllerBase
    {
        private readonly Lab08DbContext _context;

        public Ejercicio2Controller(Lab08DbContext context)
        {
            _context = context;
        }
        [HttpGet("GetProductsByMinPrice")]
        public ActionResult<IEnumerable<Product>> GetProductsByMinPrice(decimal minPrice)
        {
            if (minPrice < 0)
            {
                return BadRequest("El precio mínimo no puede ser negativo");
            }

            var products = _context.Products
                .Where(p => p.Price > minPrice)
                .OrderBy(p => p.Price)
                .ToList();

            return Ok(products);
        }
    }
}