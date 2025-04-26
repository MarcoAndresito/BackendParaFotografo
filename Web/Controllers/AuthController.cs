using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Aplication;
using Domain.DTOs;
using Domain.Models;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IUsuarioServices usuarioServices, IConfiguration configuration) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var usuario = await usuarioServices.ValidarUsuarioAsync(model);
            if (usuario == null)
            {
                return Unauthorized("contraeñas imvalidad");
            }
            var key = configuration["Jwt:Key"] ?? throw new Exception("falta configurar la llave en el appseting");
            var token = await usuarioServices.GenerarTokenAsync(usuario, key);
            return Ok(token);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistroUsuario model)
        {
            if (string.IsNullOrEmpty(model.Nombre) || string.IsNullOrEmpty(model.Correo) || string.IsNullOrEmpty(model.Contraseña))
            {
                return BadRequest("El nombre, correo y contraseña son requeridos");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var usuario = await usuarioServices.RegistaraUsuarioAsync(model);
                return Ok(new
                {
                    mensaje = "Usuario creado",
                    usuario = usuario,
                });
            }
            catch (Exception e)
            {
                //return Conflict(e);
                return BadRequest(e);
            }
        }

        [Authorize]
        [HttpGet("secure")]
        public IActionResult SecureEndpoint()
        {
            return Ok(new { message = "Este endpoint es seguro y requiere autenticación." });
        }
    }
}
