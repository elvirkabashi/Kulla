using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KULLA.Data;
using KULLA.Models;
using Microsoft.AspNetCore.Authorization;

namespace KULLA.Controllers
{
    [Authorize]
    public class FaturaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FaturaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Fatura
        public async Task<IActionResult> Index()
        {
            var faturat = await _context.Faturat.OrderByDescending(x => x.NrFatures).ToListAsync();

            foreach (var fatura in faturat)
            {
                var shitesiId = fatura.Shitesi;
                var shitesi = await _context.Users.FirstOrDefaultAsync(u => u.Id == shitesiId);
                ViewData[$"ShitesiName{fatura.NrFatures}"] = shitesi?.FullName;
            }

            return View(faturat);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fatura = await _context.Faturat.FirstOrDefaultAsync(m => m.NrFatures == id);
            if (fatura == null)
            {
                return NotFound();
            }

            // Merr listën e shitjeve për këtë faturë
            var shitjet = await _context.Shitjet.Include(s => s.Produkti).Where(s => s.NrFatures == id).ToListAsync();

            // Paraqit shitjet në shabllon
            ViewData["Shitjet"] = shitjet;

            var shitesiId = fatura.Shitesi;
            var shitesi = await _context.Users.FirstOrDefaultAsync(u => u.Id == shitesiId);
            ViewData["ShitesiName"] = shitesi?.FullName;

            return View(fatura);
        }


        // GET: Fatura/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NrFatures,Blersi,Totali")] Fatura fatura)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fatura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fatura);
        }

        // GET: Fatura/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Faturat == null)
            {
                return NotFound();
            }

            var fatura = await _context.Faturat.FindAsync(id);
            if (fatura == null)
            {
                return NotFound();
            }
            return View(fatura);
        }

        // POST: Fatura/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NrFatures,Blersi,Totali")] Fatura fatura)
        {
            if (id != fatura.NrFatures)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fatura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FaturaExists(fatura.NrFatures))
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
            return View(fatura);
        }

        // GET: Fatura/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Faturat == null)
            {
                return NotFound();
            }

            var fatura = await _context.Faturat
                .FirstOrDefaultAsync(m => m.NrFatures == id);
            if (fatura == null)
            {
                return NotFound();
            }

            return View(fatura);
        }

        // POST: Fatura/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Faturat == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Faturat'  is null.");
            }
            var fatura = await _context.Faturat.FindAsync(id);
            if (fatura != null)
            {
                _context.Faturat.Remove(fatura);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FaturaExists(int id)
        {
          return (_context.Faturat?.Any(e => e.NrFatures == id)).GetValueOrDefault();
        }
    }
}
