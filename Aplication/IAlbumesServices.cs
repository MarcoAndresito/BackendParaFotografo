
using Domain.DTOs;
using Domain.Models;

namespace Aplication;

public interface IAlbumesServices
{
    Task DeleteAsync(int id);
    Task<ExportarAlbumResponce> ExportarAsync(int albumId);
    Task<IEnumerable<Album>> GetAllAsync();
    Task<Album> GetByIdAsync(int id);
    Task<IEnumerable<Foto>> GetFotosPorAlbumAsync(int albumId);
    Task<Foto> PostFotoEnAlbumAsync(int albumId, FotoUploadRequest request);
    Task<AlbumSaveResponse> SaveAsync(AlbumSaveRequest album);
    Task<Album> UpdateAsync(Album album);
}
