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
    public async Task<ActionResult> ExportarAsync(string albumId)
    {
        string rutaBase = @"C:\upc-album";
        string rutaCarpeta = Path.Combine(rutaBase, albumId);

        // Generar nombre único con fecha y hora
        string fechaHora = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string nombreZip = $"{albumId}_{fechaHora}.zip";
        string rutaZip = Path.Combine(Path.GetTempPath(), nombreZip);

        try
        {
            if (!Directory.Exists(rutaCarpeta))
                return NotFound(new { mensaje = $"No se encontró la carpeta '{albumId}'" });

            // Comprimir la carpeta en un archivo único de manera asíncrona
            await Task.Run(() => ZipFile.CreateFromDirectory(rutaCarpeta, rutaZip));

            // Leer el archivo ZIP y devolverlo como respuesta
            var bytes = await System.IO.File.ReadAllBytesAsync(rutaZip);
            return File(bytes, "application/zip", nombreZip);
        }
        catch (UnauthorizedAccessException)
        {
            return StatusCode(403, new { mensaje = "No tienes permisos para acceder a esta carpeta" });
        }
        catch (IOException ex)
        {
            return StatusCode(500, new { mensaje = "Error de entrada/salida al comprimir la carpeta", error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { mensaje = "Error inesperado al comprimir la carpeta", error = ex.Message });
        }
    }
}
