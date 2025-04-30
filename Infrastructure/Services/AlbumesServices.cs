using Aplication;
using Domain.DTOs;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;

namespace Infrastructure.Services;

public class AlbumesServices(ApplicationDbContext context) : IAlbumesServices
{
    public async Task DeleteAsync(int id)
    {
        var album = await context.Albumes.FindAsync(id) ?? throw new Exception("no encontrado");
        context.Albumes.Remove(album);
        await context.SaveChangesAsync();
    }

    public async Task<ExportarAlbumResponce> ExportarAsync(int albumId)
    {
        var album = await context.Albumes
       .Include(a => a.Fotos)
       .FirstOrDefaultAsync(a => a.Id == albumId)
       ?? throw new Exception("Álbum no encontrado");

        var fotos = album.Fotos.ToList();

        using var memoriaZip = new MemoryStream();
        using (var zip = new ZipArchive(memoriaZip, ZipArchiveMode.Create, leaveOpen: true))
        {
            foreach (var foto in fotos)
            {
                // Suponiendo que cada foto tiene propiedades: NombreArchivo (string) y Contenido (byte[])
                var entry = zip.CreateEntry(foto.FileName);

                using var entryStream = entry.Open();
                using var fotoStream = new MemoryStream(foto.imageBytes);
                await fotoStream.CopyToAsync(entryStream);
            }
        }

        memoriaZip.Seek(0, SeekOrigin.Begin);

        string album_zip = $"album_{album.Id}_{DateTime.Now:yyyyMMddHHmmss}.zip";
        return new ExportarAlbumResponce
        {
            NombreArchivo = album_zip,
            Contenido = memoriaZip.ToArray()
        };
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

    public async Task<Foto> PostFotoEnAlbumAsync(int albumId, FotoUploadRequest request)
    {
        Foto nuevaFoto = new()
        {
            imageBytes = request.imageBytes,
            FileName = request.FileName,
            ContentType = request.ContentType,
            FechaSubida = DateTime.Now,
            AlbumId = albumId,
        };
        context.Fotos.Add(nuevaFoto);
        await context.SaveChangesAsync();
        return nuevaFoto;
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
