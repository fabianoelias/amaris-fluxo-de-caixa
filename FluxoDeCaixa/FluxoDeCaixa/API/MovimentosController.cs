using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FluxoDeCaixa.Data;
using FluxoDeCaixa.Models;
using Microsoft.AspNetCore.Authorization;

namespace FluxoDeCaixa.API
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MovimentosController : ControllerBase
    {
        private readonly ApplicationDbContext db;

        public MovimentosController(ApplicationDbContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movimento>>> GetMovimento()
        {
            if (db.Movimento == null)
            {
                return NotFound();
            }
            return await db.Movimento.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movimento>> GetMovimento(int id)
        {
            if (db.Movimento == null)
            {
                return NotFound();
            }
            var movimento = await db.Movimento.FindAsync(id);

            if (movimento == null)
            {
                return NotFound();
            }

            return movimento;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovimento(int id, Movimento movimento)
        {
            if (id != movimento.MovimentoId)
            {
                return BadRequest();
            }

            db.Entry(movimento).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovimentoExists(id))
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

        [HttpPost]
        public async Task<ActionResult<Movimento>> PostMovimento(Movimento movimento)
        {
            if (db.Movimento == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Movimento'  is null.");
            }
            db.Movimento.Add(movimento);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetMovimento", new { id = movimento.MovimentoId }, movimento);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovimento(int id)
        {
            if (db.Movimento == null)
            {
                return NotFound();
            }
            var movimento = await db.Movimento.FindAsync(id);
            if (movimento == null)
            {
                return NotFound();
            }

            db.Movimento.Remove(movimento);
            await db.SaveChangesAsync();

            return NoContent();
        }

        private bool MovimentoExists(int id)
        {
            return (db.Movimento?.Any(e => e.MovimentoId == id)).GetValueOrDefault();
        }
    }
}