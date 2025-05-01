using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Text.Json;
using Domain.DTOs;
using Web.Services;

namespace TuNombreDeProyecto.Controllers
{
    [ApiController]
    [Route("api/upload")]
    [EnableCors("AllowMyOrigin")]
    public class UploadController : ControllerBase
    {
        private readonly UploadService _uploadService;
        private readonly IWebHostEnvironment _environment;

        public UploadController(UploadService uploadService, IWebHostEnvironment environment)
        {
            _uploadService = uploadService;
            _environment = environment;
        }

        [HttpPost("uploadFoto")]
        public async Task<IActionResult> UploadFoto([FromForm] List<IFormFile> imageFile)
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
                return Ok(new { images = imageUrls, message = "Imágenes subidas correctamente" }); // Devuelve un objeto con un array
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
//fs





