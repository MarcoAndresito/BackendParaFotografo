using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Comentario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string Contenido { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public DateTime? FechaEdicion { get; set; }

        // Puede ser null si el comentario es anónimo
        public string? UsuarioId { get; set; }

        // CREANDO NUEVA PROPIEDAD PARA LA RELACIÓN CON LA FOTO
        [Required]
        public int FotoId { get; set; }

        public Comentario() { }
    }
}