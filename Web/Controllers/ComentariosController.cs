using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Aplication;
using Domain.DTOs;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComentariosController(IComentariosServices comentariosServices) : ControllerBase
{
    // GET: api/comentarios
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Comentario>>> GetAllAsync()
    {
        var resultado = await comentariosServices.GetAllAsync();
        return Ok(resultado);
    }

    // GET api/comentarios/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Comentario>> GetByIdAsync(int id)
    {
        var comentario = await comentariosServices.GetByIdAsync(id);
        if (comentario == null)
        {
            return NotFound();
        }
        return Ok(comentario);
    }

    // GET api/comentarios/Foto/{fotoId}
    [HttpGet("Foto/{fotoId}")]
    public async Task<ActionResult<IEnumerable<Comentario>>> GetByFotoIdAsync(int fotoId)
    {
        var comentariosDeFoto = await comentariosServices.GetByFotoIdAsync(fotoId);
        return Ok(comentariosDeFoto);
    }

    // POST api/comentarios
    [HttpPost]
    public async Task<ActionResult<Comentario>> PostAsync([FromBody] Comentario nuevoComentario)
    {
        if (string.IsNullOrEmpty(nuevoComentario.Contenido))
        {
            return BadRequest("El contenido del comentario es requerido");
        }

        if (nuevoComentario.FotoId == 0)
        {
            return BadRequest("El Id de la foto es requerido");
        }

        nuevoComentario.FechaCreacion = System.DateTime.Now;
        var comentarioGuardado = await comentariosServices.SaveAsync(nuevoComentario);
        return Ok(comentarioGuardado);
    }

    // PUT api/comentarios/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Comentario comentarioActualizado)
    {
        var comentarioExistente = await comentariosServices.UpdateAsync(id, comentarioActualizado);
        if (comentarioExistente == null)
        {
            return NotFound();
        }
        return Ok(comentarioExistente);
    }

    // DELETE api/comentarios/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await comentariosServices.DeleteAsync(id);
        return Ok("Comentario eliminado correctamente");
    }
}