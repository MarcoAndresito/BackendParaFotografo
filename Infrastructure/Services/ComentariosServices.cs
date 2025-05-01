// En Aplication/ComentariosServices.cs
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Aplication
{
    public class ComentariosServices : IComentariosServices
    {
        private readonly ApplicationDbContext _context;

        public ComentariosServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comentario>> GetAllAsync()
        {
            return await _context.Comentarios.ToListAsync();
        }

        public async Task<Comentario?> GetByIdAsync(int id)
        {
            return await _context.Comentarios.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Comentario>> GetByFotoIdAsync(int fotoId)
        {
            return await _context.Comentarios.Where(c => c.FotoId == fotoId).ToListAsync();
        }

        public async Task<Comentario> SaveAsync(Comentario comentario)
        {
            _context.Comentarios.Add(comentario);
            await _context.SaveChangesAsync();
            return comentario;
        }

        public async Task<Comentario?> UpdateAsync(int id, Comentario comentario)
        {
            var comentarioExistente = await _context.Comentarios.FirstOrDefaultAsync(c => c.Id == id);
            if (comentarioExistente == null)
            {
                return null;
            }

            comentarioExistente.Contenido = comentario.Contenido;
            comentarioExistente.FechaEdicion = System.DateTime.Now;
            comentarioExistente.FotoId = comentario.FotoId;
            await _context.SaveChangesAsync();
            return comentarioExistente;
        }

        public async Task DeleteAsync(int id)
        {
            var comentario = await _context.Comentarios.FirstOrDefaultAsync(c => c.Id == id);
            if (comentario != null)
            {
                _context.Comentarios.Remove(comentario);
                await _context.SaveChangesAsync();
            }
        }
    }
}