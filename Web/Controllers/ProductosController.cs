using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Web.Models;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        // Lista en memoria para simular una base de datos
        private static List<Producto> Productos = new List<Producto>
        {
            new Producto { Id = 1, Nombre = "Laptop", Precio = 1200.50m, Stock = 15 },
            new Producto { Id = 2, Nombre = "Mouse", Precio = 25.99m, Stock = 50 },
            new Producto { Id = 3, Nombre = "Teclado", Precio = 45.75m, Stock = 30 }
        };

        // GET: api/productos
        [HttpGet]
        public ActionResult<IEnumerable<Producto>> Get()
        {
            return Ok(Productos);
        }

        // GET api/productos/5
        [HttpGet("{id}")]
        public ActionResult<Producto> Get(int id)
        {
            var producto = Productos.FirstOrDefault(p => p.Id == id);

            if (producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }

        // POST api/productos
        [HttpPost]
        public ActionResult<Producto> Post([FromBody] Producto nuevoProducto)
        {
            // Validación simple
            if (string.IsNullOrEmpty(nuevoProducto.Nombre))
            {
                return BadRequest("El nombre del producto es requerido");
            }

            // Generar un nuevo ID
            var nuevoId = Productos.Max(p => p.Id) + 1;
            nuevoProducto.Id = nuevoId;

            Productos.Add(nuevoProducto);

            return CreatedAtAction(nameof(Get), new { id = nuevoId }, nuevoProducto);
        }

        // PUT api/productos/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Producto productoActualizado)
        {
            var productoExistente = Productos.FirstOrDefault(p => p.Id == id);

            if (productoExistente == null)
            {
                return NotFound();
            }

            // Actualizar propiedades
            productoExistente.Nombre = productoActualizado.Nombre;
            productoExistente.Precio = productoActualizado.Precio;
            productoExistente.Stock = productoActualizado.Stock;

            return NoContent();
        }

        // DELETE api/productos/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var producto = Productos.FirstOrDefault(p => p.Id == id);

            if (producto == null)
            {
                return NotFound();
            }

            Productos.Remove(producto);

            return NoContent();
        }
    }
}
