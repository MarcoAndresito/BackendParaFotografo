using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Data;
using Web.Models;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetAllAsync()
        {
            var resultado = await _context.Productos.ToListAsync();
            return Ok(resultado);
        }

        // GET api/productos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetByIdAsync(int id)
        {
            var producto = await _context.Productos.FirstOrDefaultAsync(p => p.Id == id);

            if (producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }

        // POST api/productos
        [HttpPost]
        public async Task<ActionResult<Producto>> PostAsync([FromBody] Producto nuevoProducto)
        {
            // Validación simple
            if (string.IsNullOrEmpty(nuevoProducto.Nombre))
            {
                return BadRequest("El nombre del producto es requerido");
            }

            await _context.Productos.AddAsync(nuevoProducto);

            await _context.SaveChangesAsync();

            return Ok(nuevoProducto);
        }

        // PUT api/productos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Producto productoActualizado)
        {
            var productoExistente = await _context.Productos.FirstOrDefaultAsync(p => p.Id == id);

            if (productoExistente == null)
            {
                return NotFound();
            }

            // Actualizar propiedades
            productoExistente.Nombre = productoActualizado.Nombre;
            productoExistente.Marca = productoActualizado.Marca;
            productoExistente.Precio = productoActualizado.Precio;
            productoExistente.Stock = productoActualizado.Stock;

            await _context.SaveChangesAsync();

            return Ok(productoExistente);
        }

        // DELETE api/productos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var producto = await _context.Productos.FirstOrDefaultAsync(p => p.Id == id);

            if (producto == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(producto);

            await _context.SaveChangesAsync();

            return Ok("eliminado correctamente");
        }
    }
}
