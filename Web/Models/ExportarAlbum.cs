//YANILA SOLIZ DURAN
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class ExportarAlbum
    {
        [Required]
        [MaxLength(100)]
        public string NombreArchivo { get; set; } = string.Empty;

        [Required]
        [MaxLength(10)]
        public string Formato { get; set; } = "JPG";

        public DateTime FechaExportacion { get; set; } = DateTime.UtcNow;

        [MaxLength(255)]
        public string EnlaceDescarga { get; set; } = string.Empty;
    }
}
