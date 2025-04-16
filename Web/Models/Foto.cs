using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web.Models { 
    public class Foto
    {
        public int Id { get; set; }
        public int AlbumId { get; set; }
        public string NombreArchivo { get; set; }
        public string Url { get; set; }
        public string Formato { get; set; }
        public int PesoKB { get; set; }
        public int AnchoPx { get; set; }
        public int AltoPx { get; set; }
        public string PublicId { get; set; }
        public DateTime FechaSubida { get; set; }
        public Album Album { get; set; }
    }
}