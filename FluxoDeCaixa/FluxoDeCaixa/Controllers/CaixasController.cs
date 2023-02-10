using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FluxoDeCaixa.Data;
using FluxoDeCaixa.Models;
using Microsoft.AspNetCore.Authorization;

namespace FluxoDeCaixa.Controllers
{
    [Authorize]
    public class CaixasController : Controller
    {
        private readonly ApplicationDbContext db;

        public CaixasController(ApplicationDbContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = db.Caixa.Include(c => c.Status).OrderByDescending(item => item.Id);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || db.Caixa == null)
            {
                return NotFound();
            }

            var caixa = await db.Caixa
                .Include(c => c.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (caixa == null)
            {
                return NotFound();
            }

            ViewBag.Movimentos = db.Movimento.Where(item => item.CaixaId == id).OrderByDescending(item => item.Cadastro).ToList();

            return View(caixa);
        }

        public IActionResult Create()
        {
            ViewData["StatusId"] = new SelectList(db.Status, "StatusId", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Saldo,StatusId")] Caixa caixa)
        {
            if (ModelState.IsValid)
            {
                db.Add(caixa);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StatusId"] = new SelectList(db.Status, "StatusId", "Nome", caixa.StatusId);
            return View(caixa);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.Caixa == null)
            {
                return NotFound();
            }

            var caixa = await db.Caixa.FindAsync(id);
            if (caixa == null)
            {
                return NotFound();
            }
            ViewData["StatusId"] = new SelectList(db.Status, "StatusId", "Nome", caixa.StatusId);
            return View(caixa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Saldo,StatusId")] Caixa caixa)
        {
            if (id != caixa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(caixa);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaixaExists(caixa.Id))
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
            ViewData["StatusId"] = new SelectList(db.Status, "StatusId", "Nome", caixa.StatusId);
            return View(caixa);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || db.Caixa == null)
            {
                return NotFound();
            }

            var caixa = await db.Caixa
                .Include(c => c.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (caixa == null)
            {
                return NotFound();
            }

            return View(caixa);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.Caixa == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Caixa'  is null.");
            }
            var caixa = await db.Caixa.FindAsync(id);
            if (caixa != null)
            {
                db.Caixa.Remove(caixa);
            }

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CaixaExists(int id)
        {
            return (db.Caixa?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}