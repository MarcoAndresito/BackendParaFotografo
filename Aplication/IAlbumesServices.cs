
using Domain.DTOs;
using Domain.Models;

namespace Aplication;

public interface IAlbumesServices
{
    Task DeleteAsync(int id);
    Task<ExportarAlbumResponce> ExportarAsync(int albumId, ExportarAlbumRequest request);
    Task<IEnumerable<Album>> GetAllAsync();
    Task<Album> GetByIdAsync(int id);
    Task<IEnumerable<Foto>> GetFotosPorAlbumAsync(int albumId);
    Task<Foto> PostFotoEnAlbumAsync(int albumId, Foto foto);
    Task<Album> SaveAsync(Album album);
    Task<Album> UpdateAsync(Album album);
}
