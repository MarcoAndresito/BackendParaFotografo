using Domain.Models;

namespace Aplication;

public interface IProductoService
{
    Task<List<Producto>> GetAllAsync();
    Task<Producto> GetByIdAsync(int id);
    Task<Producto> SaveAsync(Producto producto);
    Task<Producto> UpdateAsync(Producto producto);
    Task DeleteAsync(int id);
}
