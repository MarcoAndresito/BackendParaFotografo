using Domain.Models;

namespace Domain.DTOs;

public class AlbumSaveResponse
{
    public string mensage { get; set; }
    public Album album { get; set; }
}
