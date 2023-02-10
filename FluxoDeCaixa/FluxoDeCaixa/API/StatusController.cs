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
    public class StatusController : ControllerBase
    {
        private readonly ApplicationDbContext db;

        public StatusController(ApplicationDbContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Status>>> GetStatus()
        {
          if (db.Status == null)
          {
              return NotFound();
          }
            return await db.Status.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Status>> GetStatus(int id)
        {
          if (db.Status == null)
          {
              return NotFound();
          }
            var status = await db.Status.FindAsync(id);

            if (status == null)
            {
                return NotFound();
            }

            return status;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatus(int id, Status status)
        {
            if (id != status.StatusId)
            {
                return BadRequest();
            }

            db.Entry(status).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatusExists(id))
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
        public async Task<ActionResult<Status>> PostStatus(Status status)
        {
          if (db.Status == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Status'  is null.");
          }
            db.Status.Add(status);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetStatus", new { id = status.StatusId }, status);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            if (db.Status == null)
            {
                return NotFound();
            }
            var status = await db.Status.FindAsync(id);
            if (status == null)
            {
                return NotFound();
            }

            db.Status.Remove(status);
            await db.SaveChangesAsync();

            return NoContent();
        }

        private bool StatusExists(int id)
        {
            return (db.Status?.Any(e => e.StatusId == id)).GetValueOrDefault();
        }
    }
}