
namespace Web.Models
{
    public class Parametro
    {
        public int Id { get; set; }

        public string Servicio { get; set; }

        public string Clave { get; set; } 

        public string Valor { get; set; } 

        public string Descripcion { get; set; }

        public bool Activo { get; set; } = true;

    }
}
