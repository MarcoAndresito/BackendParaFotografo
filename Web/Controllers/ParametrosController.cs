using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/*
using Web.Data;
using Web.Models;
namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParametrosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ParametrosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/parametros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Parametro>>> GetAllAsync()
        {
            var resultado = await _context.Parametros.ToListAsync();
            return Ok(resultado);
        }

        // GET api/parametro/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Parametro>> GetByIdAsync(int id)
        {
            var producto = await _context.Parametros.FirstOrDefaultAsync(p => p.Id == id);

            if (producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }

        // POST api/parametro
        [HttpPost]
        public async Task<ActionResult<Parametro>> PostAsync([FromBody] Parametro nuevoParametro)
        {
            // Validación simple
            if (string.IsNullOrEmpty(nuevoParametro.Servicio) || string.IsNullOrEmpty(nuevoParametro.Clave))
                return BadRequest("Servicio y Clave son obligatorios");

            await _context.Parametros.AddAsync(nuevoParametro);

            await _context.SaveChangesAsync();

            return Ok(nuevoParametro);
        }

        // PUT api/parametro/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Parametro parametroActualizado)
        {
            var parametroExistente = await _context.Parametros.FirstOrDefaultAsync(p => p.Id == id);

            if (parametroExistente == null)
            {
                return NotFound();
            }

            // Actualizar propiedades
            parametroExistente.Servicio = parametroActualizado.Servicio;
            parametroExistente.Clave = parametroActualizado.Clave;
            parametroExistente.Valor = parametroActualizado.Valor;
            parametroExistente.Descripcion = parametroActualizado.Descripcion;
            parametroExistente.Activo = parametroActualizado.Activo;

            await _context.SaveChangesAsync();

            return Ok(parametroExistente);
        }

        // DELETE api/parametro/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var parametro = await _context.Parametros.FirstOrDefaultAsync(p => p.Id == id);

            if (parametro == null)
            {
                return NotFound();
            }

            _context.Parametros.Remove(parametro);

            await _context.SaveChangesAsync();

            return Ok("eliminado correctamente");
        }
    }
}
*/