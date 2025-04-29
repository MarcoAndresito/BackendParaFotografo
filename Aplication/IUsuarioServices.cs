using Domain.DTOs;
using Domain.Models;

namespace Aplication;

public interface IUsuarioServices
{
    Task<RegistroUsuario?> ValidarUsuarioAsync(LoginRequest request);
    LoginResponce GenerarToken(RegistroUsuario usuario, string secretKey);
    Task<RegistroUsuario> RegistaraUsuarioAsync(RegistroUsuario model);
}
