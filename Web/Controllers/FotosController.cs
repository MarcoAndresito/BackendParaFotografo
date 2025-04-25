using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/*
using Web.Data;
using Web.Models;
namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FotosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FotosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Fotos
        [HttpGet]
        public ActionResult<IEnumerable<Foto>> GetFotos()
        {
            return _context.Fotos.ToList();
        }

        // GET: api/Fotos/{id}
        [HttpGet("{id}")]
        public ActionResult<Foto> GetFoto(int id)
        {
            var foto = _context.Fotos.Find(id);

            if (foto == null)
            {
                return NotFound();
            }

            return foto;
        }

        // PUT: api/Fotos/{id}
        [HttpPut("{id}")]
        public IActionResult PutFoto(int id, Foto foto)
        {
            if (id != foto.Id)
            {
                return BadRequest();
            }

            _context.Entry(foto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
            {
                if (!FotoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Fotos/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteFoto(int id)
        {
            var foto = _context.Fotos.Find(id);
            if (foto == null)
            {
                return NotFound();
            }

            _context.Fotos.Remove(foto);
            _context.SaveChanges();

            return NoContent();
        }

        private bool FotoExists(int id)
        {
            return _context.Fotos.Any(e => e.Id == id);
        }
    }
}
*/