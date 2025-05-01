using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Web.Services
{
    public class UploadService
    {
        private readonly string _uploadsPath;

        public UploadService(IWebHostEnvironment environment)
        {
            _uploadsPath = Path.Combine(environment.WebRootPath, "uploads");

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

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/uploads/{fileName}";
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


