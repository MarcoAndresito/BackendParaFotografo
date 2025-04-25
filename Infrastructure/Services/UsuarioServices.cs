using Aplication;
using Domain.DTOs;
using Domain.Models;

namespace Infrastructure.Services;

public class UsuarioServices : IUsuarioServices
{
    public async Task<LoginResponce> GenerarTokenAsync(RegistroUsuario usuario, string key)
    {
        throw new NotImplementedException();
    }

    public async Task<RegistroUsuario> RegistaraUsuarioAsync(RegistroUsuario model)
    {
        throw new NotImplementedException();
    }

    public async Task<RegistroUsuario> ValidarUsuarioAsync(LoginRequest request)
    {
        throw new NotImplementedException();
    }
}
