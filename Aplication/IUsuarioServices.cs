using Domain.DTOs;
using Domain.Models;

namespace Aplication;

public interface IUsuarioServices
{
    Task<RegistroUsuario> ValidarUsuarioAsync(LoginRequest request);
    Task<LoginResponce> GenerarTokenAsync(RegistroUsuario usuario, string key);
    Task<RegistroUsuario> RegistaraUsuarioAsync(RegistroUsuario model);
}
