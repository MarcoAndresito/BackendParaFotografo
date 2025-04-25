using Aplication;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlbumesController(IAlbumesServices albumesServices) : ControllerBase
{

    // GET: api/Albumes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Album>>> GetAllAsync()
    {
        var resultado = await albumesServices.GetAllAsync();
        return Ok(resultado);
    }

    // GET: api/Albumes/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Album>> GetByIdAsync(int id)
    {
        var album = await albumesServices.GetByIdAsync(id);
        if (album == null)
        {
            return NotFound();
        }
        return Ok(album);
    }

    // POST: api/Albumes
    [HttpPost]
    public async Task<ActionResult<Album>> SaveAsync(Album album)
    {
        var albumCreated = await albumesServices.SaveAsync(album);
        return Ok(albumCreated);
    }

    // PUT: api/Albumes/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult<Album>> UpdateAsync(int id, Album album)
    {
        var albunEdited = await albumesServices.UpdateAsync(album);
        return Ok(albunEdited);
    }

    // DELETE: api/Albumes/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult<string>> DeleteAsync(int id)
    {
        await albumesServices.DeleteAsync(id);
        return Ok("eliminado correctamente");
    }

    // GET: api/Albumes/{albumId}/Fotos
    [HttpGet("{albumId}/Fotos")]
    public async Task<ActionResult<IEnumerable<Foto>>> GetFotosPorAlbumAsync(int albumId)
    {
        var resultasdo = await albumesServices.GetFotosPorAlbumAsync(albumId);
        return Ok(resultasdo);
    }

    // POST: api/Albumes/{albumId}/Fotos
    [HttpPost("{albumId}/Fotos")]
    public async Task<ActionResult<Foto>> PostFotoEnAlbumAsync(int albumId, Foto foto)
    {
        var resultasdo = await albumesServices.PostFotoEnAlbumAsync(albumId, foto);
        return Ok(resultasdo);
    }

    // POST: api/Albumes/{albumId}/Exportar
    [HttpPost("{albumId}/Exportar")]
    public async Task<ActionResult<ExportarAlbumResponce>> ExportarAsync(int albumId, ExportarAlbumRequest request)
    {
        var resultasdo = await albumesServices.ExportarAsync(albumId, request);
        return Ok(resultasdo);
    }
}
