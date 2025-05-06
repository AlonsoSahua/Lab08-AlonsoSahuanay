using Lab08_AlonsoSahuanay.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Lab08_AlonsoSahuanay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Ejercicio1Controller : ControllerBase
    {
        private readonly Lab08DbContext _context;

        public Ejercicio1Controller(Lab08DbContext context)
        {
            _context = context;
        }

        [HttpGet("GetClientsByName")]
        public ActionResult<IEnumerable<Client>> GetClientsByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("El parámetro 'name' es requerido");
            }

            var clients = _context.Clients
                .Where(c => c.Name.Contains(name))
                .ToList();

            return Ok(clients);
        }
    }
}