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
    public class CaixasController : ControllerBase
    {
        private readonly ApplicationDbContext db;

        public CaixasController(ApplicationDbContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Caixa>>> GetCaixa()
        {
            if (db.Caixa == null)
            {
                return NotFound();
            }
            return await db.Caixa.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Caixa>> GetCaixa(int id)
        {
            if (db.Caixa == null)
            {
                return NotFound();
            }
            var caixa = await db.Caixa.FindAsync(id);

            if (caixa == null)
            {
                return NotFound();
            }

            return caixa;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCaixa(int id, Caixa caixa)
        {
            if (id != caixa.Id)
            {
                return BadRequest();
            }

            db.Entry(caixa).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaixaExists(id))
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
        public async Task<ActionResult<Caixa>> PostCaixa(Caixa caixa)
        {
            if (db.Caixa == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Caixa'  is null.");
            }
            db.Caixa.Add(caixa);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetCaixa", new { id = caixa.Id }, caixa);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCaixa(int id)
        {
            if (db.Caixa == null)
            {
                return NotFound();
            }
            var caixa = await db.Caixa.FindAsync(id);
            if (caixa == null)
            {
                return NotFound();
            }

            db.Caixa.Remove(caixa);
            await db.SaveChangesAsync();

            return NoContent();
        }

        private bool CaixaExists(int id)
        {
            return (db.Caixa?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}