using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Domain.Models
{
    public class RegistroUsuario
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="El nombre es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El correo es requerido")]
        [EmailAddress(ErrorMessage ="el formato del correo es invalido")]
        public string Correo { get; set; }
        [Required]
        public string Contraseña { get; set; }
    }
}