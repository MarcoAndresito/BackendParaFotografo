using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Services; // Asegúrate que esté correctamente referenciado

[ApiController]
[Route("api/upload")]
[EnableCors("AllowMyOrigin")]
public class UploadController : ControllerBase
{
    private readonly UploadService _uploadService;

    public UploadController(UploadService uploadService)
    {
        _uploadService = uploadService;
    }

    [HttpPost("uploadFoto")]
    public async Task<IActionResult> UploadFotos([FromForm] List<IFormFile> imageFile)
    {
        if (imageFile == null || imageFile.Count == 0)
        {
            return BadRequest("No se proporcionaron imágenes.");
        }

        try
        {
            var imageUrls = new List<string>();

            foreach (var file in imageFile)
            {
                var imageUrl = await _uploadService.SaveImageAndGetUrl(file);
                imageUrls.Add(imageUrl);
            }

            return Ok(imageUrls); // Devuelve un array con URLs
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al subir imágenes: {ex.Message}");
        }
    }

    [HttpGet("images")]
    public IActionResult GetImages()
    {
        var imageUrls = _uploadService.GetAllImageUrls();
        return Ok(imageUrls);
    }
}


