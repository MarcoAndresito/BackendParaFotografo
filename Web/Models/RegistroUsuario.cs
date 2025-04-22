using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class RegistroUsuario
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Correo { get; set; }
        [Required]
        public string Contraseña { get; set; }
    }
}