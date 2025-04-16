//YANILA SOLIZ DURAN
namespace Web.Models
{
    public class ExportarAlbum
    {
        public int Id { get; set; }
        public string NombreArchivo { get; set; } = string.Empty;
        public string Formato { get; set; } = "JPG";
        public DateTime FechaExportacion { get; set; } = DateTime.UtcNow;
        public string EnlaceDescarga { get; set; } = string.Empty;
    }
}
