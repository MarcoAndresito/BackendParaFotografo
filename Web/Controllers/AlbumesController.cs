﻿using Aplication;
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
    public async Task<ActionResult<IEnumerable<ListaFotosResponce>>> GetFotosPorAlbumAsync(int albumId)

    {
        var fotos = await albumesServices.GetFotosPorAlbumAsync(albumId);

        var resultado = fotos.Select(f => new ListaFotosResponce()
        {
            id = f.Id,
            nombreArchivo = f.FileName,
            formato = f.ContentType,
            fechaSubida = f.FechaSubida,
            
        });

        return Ok(resultado);
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

    // GET: api/Albumes/{albumId}/Exportar
    [HttpGet("{albumId}/Exportar")]
    public async Task<IActionResult> ExportarAsync(int albumId)
    {
        var resultado = await albumesServices.ExportarAsync(albumId);
        return File(resultado.Contenido, "application/zip", resultado.NombreArchivo);
    }
}