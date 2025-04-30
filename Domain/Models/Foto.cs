using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Foto
    {
        public int Id { get; set; }
        public int AlbumId { get; set; }
        public byte[] imageBytes { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public DateTime FechaSubida { get; set; }
        public Album Album { get; set; }
    }
}