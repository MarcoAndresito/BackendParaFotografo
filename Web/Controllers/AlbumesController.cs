using Aplication;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;
using System.Net.WebSockets;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        var resultasdo = await albumesServices.PostFotoEnAlbumAsync(albumId, new FotoUploadRequest());
        return Ok(resultasdo);
    }

    // GET: api/Albumes/{albumId}/Exportar
    [HttpGet("{albumId}/Exportar")]
    public async Task<ActionResult<ExportarAlbumResponce>> ExportarAsync(string albumId, [FromBody] ExportarAlbumRequest request)
    {
        string ruta = @"C:\upc-album";
        string rutaCarpeta = Path.Combine(ruta, albumId);
        string rutaZip = Path.Combine(Path.GetTempPath(), albumId + ".zip"); // Guarda en una carpeta temporal

        try
        {
            // Buscar carpeta específica dentro de C: que coincida con el nombre
            string[] directorios = Directory.GetDirectories(ruta, "*" + albumId + "*", SearchOption.AllDirectories);

            if (directorios.Length == 0)
                return NotFound(new { mensaje = $"No se encontró ninguna carpeta con el nombre '{albumId}'" });

            ZipFile.CreateFromDirectory(rutaCarpeta, rutaZip);
            var bytes = System.IO.File.ReadAllBytes(rutaZip);
            return File(bytes, "application/zip", albumId + ".zip");
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { mensaje = "Error al buscar directorios", error = ex.Message });
        }
    }
}
