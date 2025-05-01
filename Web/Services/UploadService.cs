using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Web.Services
{
   public class UploadService
{
    private readonly string _uploadsPath;
    private readonly IWebHostEnvironment _environment;

    public UploadService(IWebHostEnvironment environment)
    {
        _environment = environment; // Guarda la referencia al entorno.
        _uploadsPath = Path.Combine(_environment.WebRootPath, "uploads"); // Usa la referencia guardada.

            if (!Directory.Exists(_uploadsPath))
            {
                Directory.CreateDirectory(_uploadsPath);
            }
        }

        public async Task<string> SaveImageAndGetUrl(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new Exception("Archivo inválido");

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(_uploadsPath, fileName);
    try
    {
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return $"/uploads/{fileName}";
    }
    catch (Exception ex)
    {
        // Loguea el error aquí también
        Console.WriteLine($"Error en SaveImageAndGetUrl: {ex.Message}");
        throw new Exception("Error al guardar la imagen: " + ex.Message, ex); // Incluye la excepción original
    }
}
        public List<string> GetAllImageUrls()
        {
            var urls = new List<string>();

            foreach (var path in Directory.GetFiles(_uploadsPath))
            {
                var fileName = Path.GetFileName(path);
                urls.Add($"/uploads/{fileName}");
            }

            return urls;
        }
    }
}

