using Aplication;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegistroUsuarioController(IUsuarioServices usuarioServices) : ControllerBase
{

    // POST api/registrousuario
    [HttpPost]
    public async Task<ActionResult<RegistroUsuario>> PostAsync([FromBody] RegistroUsuario nuevoRegistroUsuario)
    {
        // Validación de los datos de entrada 
        if (string.IsNullOrEmpty(nuevoRegistroUsuario.Nombre) || string.IsNullOrEmpty(nuevoRegistroUsuario.Correo) || string.IsNullOrEmpty(nuevoRegistroUsuario.Contraseña))
        {
            return BadRequest("El nombre, correo y contraseña son requeridos");
        }

        var usuario = await usuarioServices.RegistaraUsuarioAsync(nuevoRegistroUsuario);

        return Ok(new
        {
            mensaje = "Usuario creado",
            usuario = usuario
        });
    }
}