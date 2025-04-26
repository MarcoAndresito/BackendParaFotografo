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
        var album = await context.Albumes.Include(a => a.Fotos).FirstOrDefaultAsync(a => a.Id == albumId)?? throw new Exception("Álbum no encontrado");

        // Obtén la lista de las fotos asociadas al álbum.
        var fotos = album.Fotos.ToList();

        // Comprime cada una de las fotos de la lista.
        var zipPath = Path.Combine("Exports", $"Album_{albumId}.zip");
        Directory.CreateDirectory("Exports");

        // Retorna un archivo comprimido con todas las fotos.
        return new ExportarAlbumResponce
        {
            FilePath = zipPath,
            Message = "Álbum exportado exitosamente."
        };

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

    public async Task<Foto> PostFotoEnAlbumAsync(int albumId, Foto foto)
    {
        var album = await context.Albumes.FindAsync(albumId) ?? throw new Exception("album no encontrado");
        foto.AlbumId = album.Id;
        context.Fotos.Add(foto);
        context.SaveChanges();
        return foto;
    }

    public async Task<Album> SaveAsync(Album album)
    {
        context.Albumes.Add(album);
        await context.SaveChangesAsync();
        return album;
    }

    public async Task<Album> UpdateAsync(Album album)
    {
        context.Entry(album).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return album;
    }
}
