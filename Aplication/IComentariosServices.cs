// En Aplication/IComentariosServices.cs
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplication
{
    public interface IComentariosServices
    {
        Task<IEnumerable<Comentario>> GetAllAsync();
        Task<Comentario?> GetByIdAsync(int id);
        Task<IEnumerable<Comentario>> GetByFotoIdAsync(int fotoId);
        Task<Comentario> SaveAsync(Comentario comentario);
        Task<Comentario?> UpdateAsync(int id, Comentario comentario);
        Task DeleteAsync(int id);
    }
}