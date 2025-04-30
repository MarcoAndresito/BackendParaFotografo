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
    public async Task<ActionResult<AlbumSaveResponse>> SaveAsync(AlbumSaveRequest album)
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
    public async Task<ActionResult<Foto>> PostFotoEnAlbumAsync(int albumId, IFormFile imageFile)
    {
        if (imageFile == null || imageFile.Length == 0)
            return BadRequest("No se proporcionó ninguna imagen.");
        FotoUploadRequest request = new()
        {
            FileName = imageFile.FileName,
            ContentType = imageFile.ContentType

        };
        using (var memoryStream = new MemoryStream())
        {
            await imageFile.CopyToAsync(memoryStream);
            request.imageBytes = memoryStream.ToArray();
        }

        var resultasdo = await albumesServices.PostFotoEnAlbumAsync(albumId, request);
        return Ok(resultasdo);
    }

    // POST: api/Albumes/{albumId}/Exportar
    [HttpPost("{albumId}/Exportar")]
    public async Task<ActionResult<ExportarAlbumResponce>> ExportarAsync(int albumId, [FromBody] ExportarAlbumRequest request)
    {
        var resultasdo = await albumesServices.ExportarAsync(albumId, request);
        return Ok(resultasdo);
    }
}
