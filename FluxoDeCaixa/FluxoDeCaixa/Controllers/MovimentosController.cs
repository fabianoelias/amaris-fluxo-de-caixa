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

        public IActionResult Create(int id)
        {
            ViewData["CaixaId"] = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovimentoId,Valor,Sucesso,Motivo,Detalhe,CaixaId,Cadastro")] Movimento movimento)
        {
            var caixa = db.Caixa.Find(movimento.CaixaId);
            if (caixa is null)
            {
                return NotFound();
            }
            var status = db.Status.Find(caixa.StatusId);
            if (status is null)
            {
                return NotFound();
            }
            if (status.Nome.Equals("Fechado"))
            {
                movimento.Detalhe = $"Caixa {status.Nome}";
                movimento.Sucesso = false;
            }
            else
            {
                var saldo = caixa.Saldo;
                saldo += movimento.Valor;
                if (saldo < 0)
                {
                    movimento.Detalhe = $"Valor a debitar {movimento.Valor * -1} maior que o Saldo {caixa.Saldo}";
                    movimento.Sucesso = false;
                }
                else
                {
                    caixa.Saldo = saldo;
                    movimento.Detalhe = $"Valor {movimento.Valor} movimentado com sucesso";
                    movimento.Sucesso = true;
                }
            }
            db.Caixa.Update(caixa);
            db.SaveChanges();
            movimento.Cadastro = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Add(movimento);
                await db.SaveChangesAsync();
                return RedirectToAction("Details", "Caixas", new { id = movimento.CaixaId });
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
        public async Task<IActionResult> Edit(int id, [Bind("MovimentoId,Valor,Sucesso,Motivo,Detalhe,CaixaId,Cadastro")] Movimento movimento)
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