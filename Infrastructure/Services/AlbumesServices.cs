using Aplication;
using Domain.DTOs;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class AlbumesServices(ApplicationDbContext context) : IAlbumesServices
{
    public async Task DeleteAsync(int id)
    {
        var album = await context.Albumes.FindAsync(id) ?? throw new Exception("no encontrado");
        context.Albumes.Remove(album);
        await context.SaveChangesAsync();
    }

    public async Task<ExportarAlbumResponce> ExportarAsync(int albumId, ExportarAlbumRequest request)
    {
        // Con el ID, busca el álbum en la base de datos.
        // Obtén la lista de las fotos asociadas al álbum.
        // Comprime cada una de las fotos de la lista.
        // Retorna un archivo comprimido con todas las fotos.
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Album>> GetAllAsync()
    {
        return await context.Albumes.ToListAsync();
    }

    public async Task<Album> GetByIdAsync(int id)
    {
        var album = await context.Albumes.FindAsync(id) ?? throw new Exception("no encontrado");
        return album;
    }

    public async Task<IEnumerable<Foto>> GetFotosPorAlbumAsync(int albumId)
    {
        var album = await context.Albumes.FindAsync(albumId) ?? throw new Exception("album no encontrado");
        var fotos = await context.Fotos.Where(f => f.AlbumId == albumId).ToListAsync() ?? throw new Exception("fotos no encontrado");
        return fotos;
    }

    public async Task<Foto> PostFotoEnAlbumAsync(int albumId, FotoUploadRequest foto)
    {
        throw new NotImplementedException();
    }

    public async Task<AlbumSaveResponse> SaveAsync(AlbumSaveRequest request)
    {
        Album album = new Album()
        {
            Nombre = request.Nombre,
            Descripcion = request.Descripcion,
            FechaCreacion = request.FechaCreacion,
            UsuarioId = request.UsuarioId,
        };
        context.Albumes.Add(album);
        await context.SaveChangesAsync();
        return new AlbumSaveResponse()
        {
            mensage = "Album creado correctamente",
            album = album,
        };
    }

    public async Task<Album> UpdateAsync(Album album)
    {
        context.Entry(album).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return album;
    }
}
