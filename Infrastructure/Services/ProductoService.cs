using Aplication;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ProductoService(ApplicationDbContext context) : IProductoService
{

    public async Task<List<Producto>> GetAllAsync()
    {
        var resultado = await context.Productos.ToListAsync();
        return resultado;
    }

    public async Task<Producto> GetByIdAsync(int id)
    {
        var producto = await context.Productos.FirstOrDefaultAsync(p => p.Id == id) ?? throw new Exception("no se encontro");
        return producto;
    }

    public async Task<Producto> SaveAsync(Producto producto)
    {
        await context.Productos.AddAsync(producto);
        await context.SaveChangesAsync();
        return producto;
    }

    public async Task<Producto> UpdateAsync(Producto producto)
    {
        var productoExistente = await context.Productos.FirstOrDefaultAsync(p => p.Id == producto.Id) ?? throw new Exception("no se encontro");
        productoExistente.Nombre = producto.Nombre;
        productoExistente.Marca = producto.Marca;
        productoExistente.Precio = producto.Precio;
        productoExistente.Stock = producto.Stock;
        await context.SaveChangesAsync();
        return productoExistente;
    }
    public async Task DeleteAsync(int id)
    {
        var producto = await context.Productos.FirstOrDefaultAsync(p => p.Id == id) ?? throw new Exception("no se encontro");
        context.Productos.Remove(producto);
        await context.SaveChangesAsync();
    }
}
