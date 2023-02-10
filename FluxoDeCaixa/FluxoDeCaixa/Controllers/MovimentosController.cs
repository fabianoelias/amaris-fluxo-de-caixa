using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FluxoDeCaixa.Data;
using FluxoDeCaixa.Models;
using Microsoft.AspNetCore.Authorization;

namespace FluxoDeCaixa.Controllers
{
    [Authorize]
    public class MovimentosController : Controller
    {
        private readonly ApplicationDbContext db;

        public MovimentosController(ApplicationDbContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = db.Movimento.Include(m => m.Caixa);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || db.Movimento == null)
            {
                return NotFound();
            }

            var movimento = await db.Movimento
                .Include(m => m.Caixa)
                .FirstOrDefaultAsync(m => m.MovimentoId == id);
            if (movimento == null)
            {
                return NotFound();
            }

            return View(movimento);
        }

        public IActionResult Create()
        {
            ViewData["CaixaId"] = new SelectList(db.Caixa, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovimentoId,Valor,Sucesso,Motivo,Detalhe,CaixaId")] Movimento movimento)
        {
            if (ModelState.IsValid)
            {
                db.Add(movimento);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CaixaId"] = new SelectList(db.Caixa, "Id", "Id", movimento.CaixaId);
            return View(movimento);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.Movimento == null)
            {
                return NotFound();
            }

            var movimento = await db.Movimento.FindAsync(id);
            if (movimento == null)
            {
                return NotFound();
            }
            ViewData["CaixaId"] = new SelectList(db.Caixa, "Id", "Id", movimento.CaixaId);
            return View(movimento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovimentoId,Valor,Sucesso,Motivo,Detalhe,CaixaId")] Movimento movimento)
        {
            if (id != movimento.MovimentoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(movimento);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovimentoExists(movimento.MovimentoId))
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
            ViewData["CaixaId"] = new SelectList(db.Caixa, "Id", "Id", movimento.CaixaId);
            return View(movimento);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || db.Movimento == null)
            {
                return NotFound();
            }

            var movimento = await db.Movimento
                .Include(m => m.Caixa)
                .FirstOrDefaultAsync(m => m.MovimentoId == id);
            if (movimento == null)
            {
                return NotFound();
            }

            return View(movimento);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.Movimento == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Movimento'  is null.");
            }
            var movimento = await db.Movimento.FindAsync(id);
            if (movimento != null)
            {
                db.Movimento.Remove(movimento);
            }

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovimentoExists(int id)
        {
            return (db.Movimento?.Any(e => e.MovimentoId == id)).GetValueOrDefault();
        }
    }
}