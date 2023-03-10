using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FluxoDeCaixa.Data;
using FluxoDeCaixa.Models;
using Microsoft.AspNetCore.Authorization;

namespace FluxoDeCaixa.Controllers
{
    [Authorize]
    public class StatusController : Controller
    {
        private readonly ApplicationDbContext db;

        public StatusController(ApplicationDbContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            return db.Status != null ?
                        View(await db.Status.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Status'  is null.");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || db.Status == null)
            {
                return NotFound();
            }

            var status = await db.Status
                .FirstOrDefaultAsync(m => m.StatusId == id);
            if (status == null)
            {
                return NotFound();
            }

            return View(status);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StatusId,Nome")] Status status)
        {
            if (ModelState.IsValid)
            {
                db.Add(status);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(status);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.Status == null)
            {
                return NotFound();
            }

            var status = await db.Status.FindAsync(id);
            if (status == null)
            {
                return NotFound();
            }
            return View(status);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StatusId,Nome")] Status status)
        {
            if (id != status.StatusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(status);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatusExists(status.StatusId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(status);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || db.Status == null)
            {
                return NotFound();
            }

            var status = await db.Status
                .FirstOrDefaultAsync(m => m.StatusId == id);
            if (status == null)
            {
                return NotFound();
            }

            return View(status);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.Status == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Status'  is null.");
            }
            var status = await db.Status.FindAsync(id);
            if (status != null)
            {
                db.Status.Remove(status);
            }

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatusExists(int id)
        {
            return (db.Status?.Any(e => e.StatusId == id)).GetValueOrDefault();
        }
    }
}