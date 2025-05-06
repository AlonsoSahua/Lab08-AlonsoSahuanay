using Lab08_AlonsoSahuanay.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Lab08_AlonsoSahuanay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Ejercicio5Controller : ControllerBase
    {
        private readonly Lab08DbContext _context;

        public Ejercicio5Controller(Lab08DbContext context)
        {
            _context = context;
        }

        [HttpGet("GetMostExpensiveProduct")]
        public ActionResult<Product> GetMostExpensiveProduct()
        {
            var mostExpensiveProduct = _context.Products
                .OrderByDescending(p => p.Price)
                .FirstOrDefault();

            if (mostExpensiveProduct == null)
            {
                return NotFound("No se encontraron productos en la base de datos");
            }

            return Ok(mostExpensiveProduct);
        }
    }
}