using Aplication;
using Domain.DTOs;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class UsuarioServices(ApplicationDbContext context) : IUsuarioServices
{
    public async Task<LoginResponce> GenerarTokenAsync(RegistroUsuario usuario, string key)
    {
        throw new NotImplementedException();
    }

    public async Task<RegistroUsuario> RegistaraUsuarioAsync(RegistroUsuario nuevoRegistroUsuario)
    {
        if (await context.RegistroUsuarios.AnyAsync(u => u.Correo == nuevoRegistroUsuario.Correo))
        {
            throw new Exception("El correo electrónico ya está registrado.");
        }
        await context.RegistroUsuarios.AddAsync(nuevoRegistroUsuario);
        await context.SaveChangesAsync();
        return nuevoRegistroUsuario;
    }

    public async Task<RegistroUsuario> ValidarUsuarioAsync(LoginRequest request)
    {
        throw new NotImplementedException();
    }
}
