using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Data;
using Web.Models;

namespace Web.Controllers
{ 
[ApiController]
[Route("api/[controller]")]
public class AlbumesController : ControllerBase
{
    private readonly ApplicationDbContext _context; // Inyección del contexto de la base de datos

    public AlbumesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Albumes
    [HttpGet]
    public ActionResult<IEnumerable<Album>> GetAlbumes()
    {
        return _context.Albumes.ToList();
    }

    // GET: api/Albumes/{id}
    [HttpGet("{id}")]
    public ActionResult<Album> GetAlbum(int id)
    {
        var album = _context.Albumes.Find(id);

        if (album == null)
        {
            return NotFound();
        }

        return album;
    }

    // POST: api/Albumes
    [HttpPost]
    public ActionResult<Album> PostAlbum(Album album)
    {
        _context.Albumes.Add(album);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetAlbum), new { id = album.Id }, album);
    }

    // PUT: api/Albumes/{id}
    [HttpPut("{id}")]
    public IActionResult PutAlbum(int id, Album album)
    {
        if (id != album.Id)
        {
            return BadRequest();
        }

        _context.Entry(album).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

        try
        {
            _context.SaveChanges();
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
        {
            if (!AlbumExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/Albumes/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteAlbum(int id)
    {
        var album = _context.Albumes.Find(id);
        if (album == null)
        {
            return NotFound();
        }

        _context.Albumes.Remove(album);
        _context.SaveChanges();

        return NoContent();
    }

    private bool AlbumExists(int id)
    {
        return _context.Albumes.Any(e => e.Id == id);
    }

    // GET: api/Albumes/{albumId}/Fotos
    [HttpGet("{albumId}/Fotos")]
    public ActionResult<IEnumerable<Foto>> GetFotosPorAlbum(int albumId)
    {
        var album = _context.Albumes.Find(albumId);

        if (album == null)
        {
            return NotFound();
        }

        var fotos = _context.Fotos.Where(f => f.AlbumId == albumId).ToList();
        return fotos;
    }

    // POST: api/Albumes/{albumId}/Fotos
    [HttpPost("{albumId}/Fotos")]
    public ActionResult<Foto> PostFotoEnAlbum(int albumId, Foto foto)
    {
        var album = _context.Albumes.Find(albumId);

        if (album == null)
        {
            return NotFound();
        }

        foto.AlbumId = albumId; // Aseguramos que la foto pertenezca al álbum correcto
        _context.Fotos.Add(foto);
        _context.SaveChanges();

        return CreatedAtAction("GetFoto", "Fotos", new { id = foto.Id }, foto);
    }
}
}