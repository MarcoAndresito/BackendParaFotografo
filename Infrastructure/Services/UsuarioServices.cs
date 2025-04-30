using Aplication;
using Domain.DTOs;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services;

public class UsuarioServices(ApplicationDbContext context) : IUsuarioServices
{
    public LoginResponce GenerarToken(RegistroUsuario usuario, string secretKey)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, usuario.Nombre),
            new Claim(ClaimTypes.Email, usuario.Correo),
            new Claim("nombreCompleto", usuario.Nombre+usuario.Nombre+usuario.Nombre+usuario.Nombre),
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
        );
        var response = new LoginResponce()
        {
            token = new JwtSecurityTokenHandler().WriteToken(token)
        };
        return response;
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

    public async Task<RegistroUsuario?> ValidarUsuarioAsync(LoginRequest request)
    {
        var usuario = await context.RegistroUsuarios.FirstOrDefaultAsync(u => u.Correo == request.Correo && u.Contraseña == request.Contraseña);
        return usuario;
    }
}
