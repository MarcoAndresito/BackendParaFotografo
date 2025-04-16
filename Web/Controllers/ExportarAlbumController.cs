// YANILA SOLIZ DURAN
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExportarAlbumController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExportarAlbumController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET api/exportaralbum
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExportarAlbum>>> GetAllAsync()
        {
            var exportaciones = await _context.Set<ExportarAlbum>().ToListAsync();
            return Ok(exportaciones);
        }

        // GET api/exportaralbum/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExportarAlbum>> GetByIdAsync(int id)
        {
            var exportacion = await _context.Set<ExportarAlbum>().FindAsync(id);

            if (exportacion == null)
                return NotFound();

            return Ok(exportacion);
        }

        // POST api/exportaralbum
        [HttpPost]
        public async Task<ActionResult<ExportarAlbum>> ExportarFotoAsync([FromBody] ExportarAlbum solicitud)
        {
            if (string.IsNullOrWhiteSpace(solicitud.NombreArchivo))
                return BadRequest("Nombre del archivo requerido");

            // Simular la generación de enlace de descarga
            solicitud.EnlaceDescarga = $"https://miapp.com/descargas/{Guid.NewGuid()}.zip";
            solicitud.FechaExportacion = DateTime.UtcNow;

            await _context.AddAsync(solicitud);
            await _context.SaveChangesAsync();

            return Ok(solicitud);
        }

        // DELETE api/exportaralbum/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var exportacion = await _context.Set<ExportarAlbum>().FindAsync(id);

            if (exportacion == null)
                return NotFound();

            _context.Remove(exportacion);
            await _context.SaveChangesAsync();

            return Ok("Exportación eliminada correctamente");
        }
    }
}
