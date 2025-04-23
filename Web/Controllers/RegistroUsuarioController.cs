using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Data;
using Web.Models;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistroUsuarioController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RegistroUsuarioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST api/registrousuario
        [HttpPost]
        public async Task<ActionResult<RegistroUsuario>> PostAsync([FromBody] RegistroUsuario nuevoRegistroUsuario)
        {
            // Validación de los datos de entrada 
            if (string.IsNullOrEmpty(nuevoRegistroUsuario.Nombre) || string.IsNullOrEmpty(nuevoRegistroUsuario.Correo) || string.IsNullOrEmpty(nuevoRegistroUsuario.Contraseña))
            {
                return BadRequest("El nombre, correo y contraseña son requeridos");
            }

            await _context.RegistroUsuarios.AddAsync(nuevoRegistroUsuario);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Usuario creado",
                usuario = nuevoRegistroUsuario
            });
        }
    }
}