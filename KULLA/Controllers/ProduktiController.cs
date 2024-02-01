using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KULLA.Data;
using KULLA.Models;

namespace KULLA.Controllers
{
    public class ProduktiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProduktiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Produkti
        public async Task<IActionResult> Index()
        {
              return _context.Produktet != null ? 
                          View(await _context.Produktet.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Produktet'  is null.");
        }

        // GET: Produkti/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Produktet == null)
            {
                return NotFound();
            }

            var produkti = await _context.Produktet
                .FirstOrDefaultAsync(m => m.ProduktiId == id);
            if (produkti == null)
            {
                return NotFound();
            }

            return View(produkti);
        }

        // GET: Produkti/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Produkti/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProduktiId,Emri,Cmimi,Category")] Produkti produkti)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produkti);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produkti);
        }

        // GET: Produkti/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Produktet == null)
            {
                return NotFound();
            }

            var produkti = await _context.Produktet.FindAsync(id);
            if (produkti == null)
            {
                return NotFound();
            }
            return View(produkti);
        }

        // POST: Produkti/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProduktiId,Emri,Cmimi,Category")] Produkti produkti)
        {
            if (id != produkti.ProduktiId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produkti);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProduktiExists(produkti.ProduktiId))
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
            return View(produkti);
        }

        // GET: Produkti/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Produktet == null)
            {
                return NotFound();
            }

            var produkti = await _context.Produktet
                .FirstOrDefaultAsync(m => m.ProduktiId == id);
            if (produkti == null)
            {
                return NotFound();
            }

            return View(produkti);
        }

        // POST: Produkti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Produktet == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Produktet'  is null.");
            }
            var produkti = await _context.Produktet.FindAsync(id);
            if (produkti != null)
            {
                _context.Produktet.Remove(produkti);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProduktiExists(int id)
        {
          return (_context.Produktet?.Any(e => e.ProduktiId == id)).GetValueOrDefault();
        }
    }
}
