using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Aplication;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductosController(IProductoService productoService) : ControllerBase
{
    // GET: api/productos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Producto>>> GetAllAsync()
    {
        var resultado = await productoService.GetAllAsync();
        return Ok(resultado);
    }

    // GET api/productos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Producto>> GetByIdAsync(int id)
    {
        var producto = await productoService.GetByIdAsync(id);
        if (producto == null)
        {
            return NotFound();
        }
        return Ok(producto);
    }

    // POST api/productos
    [HttpPost]
    public async Task<ActionResult<Producto>> SaveAsync([FromBody] Producto nuevoProducto)
    {
        // Validación simple
        if (string.IsNullOrEmpty(nuevoProducto.Nombre))
        {
            return BadRequest("El nombre del producto es requerido");
        }
        var producto = await productoService.SaveAsync(nuevoProducto);
        return Ok(producto);
    }

    // PUT api/productos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] Producto productoActualizado)
    {
        var productoExistente = await productoService.UpdateAsync(productoActualizado);
        return Ok(productoExistente);
    }

    // DELETE api/productos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await productoService.DeleteAsync(id);
        return Ok("eliminado correctamente");
    }
}
