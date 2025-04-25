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
    public class ComentariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ComentariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Comentarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comentario>>> GetAllAsync()
        {
            var resultado = await _context.Comentarios.ToListAsync();
            return Ok(resultado);
        }

        // GET api/Comentarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comentario>> GetByIdAsync(int id)
        {
            var comentario = await _context.Comentarios.FirstOrDefaultAsync(c => c.Id == id);

            if (comentario == null)
            {
                return NotFound();
            }

            return Ok(comentario);
        }

        // POST api/Comentarios
        [HttpPost]
        public async Task<ActionResult<Comentario>> PostAsync([FromBody] Comentario nuevoComentario)
        {
            if (string.IsNullOrEmpty(nuevoComentario.Contenido))
            {
                return BadRequest("El contenido del comentario es requerido");
            }

            await _context.Comentarios.AddAsync(nuevoComentario);
            await _context.SaveChangesAsync();

            return Ok(nuevoComentario);
        }

        // PUT api/Comentarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Comentario comentarioActualizado)
        {
            var comentarioExistente = await _context.Comentarios.FirstOrDefaultAsync(c => c.Id == id);

            if (comentarioExistente == null)
            {
                return NotFound();
            }

            // Actualizar propiedades permitidas
            comentarioExistente.Contenido = comentarioActualizado.Contenido;
            comentarioExistente.FechaEdicion = DateTime.Now; // Establecer la fecha de edición
            await _context.SaveChangesAsync();

            return Ok(comentarioExistente);
        }

        // DELETE api/Comentarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var comentario = await _context.Comentarios.FirstOrDefaultAsync(c => c.Id == id);

            if (comentario == null)
            {
                return NotFound();
            }

            _context.Comentarios.Remove(comentario);
            await _context.SaveChangesAsync();

            return Ok("Comentario eliminado correctamente");
        }
    }
}
*/