
namespace Domain.DTOs;

public class AlbumSaveRequest
{
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public DateTime FechaCreacion { get; set; }
    public string UsuarioId { get; set; }
}
