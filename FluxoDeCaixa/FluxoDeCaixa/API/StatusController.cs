using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FluxoDeCaixa.Data;
using FluxoDeCaixa.Models;

namespace FluxoDeCaixa.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Status>>> GetStatus()
        {
          if (_context.Status == null)
          {
              return NotFound();
          }
            return await _context.Status.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Status>> GetStatus(int id)
        {
          if (_context.Status == null)
          {
              return NotFound();
          }
            var status = await _context.Status.FindAsync(id);

            if (status == null)
            {
                return NotFound();
            }

            return status;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatus(int id, Status status)
        {
            if (id != status.Id)
            {
                return BadRequest();
            }

            _context.Entry(status).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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
          if (_context.Status == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Status'  is null.");
          }
            _context.Status.Add(status);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStatus", new { id = status.Id }, status);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            if (_context.Status == null)
            {
                return NotFound();
            }
            var status = await _context.Status.FindAsync(id);
            if (status == null)
            {
                return NotFound();
            }

            _context.Status.Remove(status);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StatusExists(int id)
        {
            return (_context.Status?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}