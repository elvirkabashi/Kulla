using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KULLA.Data;
using KULLA.Models;
using KULLA.Models.dto;

namespace KULLA.Controllers
{
    public class ShitjaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShitjaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Shitja
        public async Task<IActionResult> Index()
        {
            ViewBag.ProduktetList = new SelectList(await _context.Produktet.ToListAsync(), "ProduktiId", "Emri");
            var applicationDbContext = _context.Shitjet.Include(s => s.Produkti);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Shitja/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Shitjet == null)
            {
                return NotFound();
            }

            var shitja = await _context.Shitjet
                .Include(s => s.Produkti)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shitja == null)
            {
                return NotFound();
            }

            return View(shitja);
        }

        public IActionResult Create()
        {
            var numriFatures = MerrNumrinEFunditTeFaturesAsync().Result; // Merr numrin e fatures
            ViewData["ProduktetList"] = new SelectList(_context.Produktet, "ProduktiId", "Emri");
            ViewData["Shitjet"] = _context.Shitjet.Where(s => s.NrFatures == numriFatures + 1).ToList(); // Merr vetëm shitjet me numrin e fatures të caktuar
            return View(new KrijoShitjeDto());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProduktiId,Sasia,Blersi")] KrijoShitjeDto shitjaDto)
        {
            if (ModelState.IsValid)
            {
                var numriFatures = await MerrNumrinEFunditTeFaturesAsync();

                var shitja = new Shitja
                {
                    ProduktiId = shitjaDto.ProduktiId,
                    Sasia = shitjaDto.Sasia,
                    Blersi = shitjaDto.Blersi,
                    NrFatures = numriFatures + 1
                };

                _context.Add(shitja);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
            ViewData["ProduktetList"] = new SelectList(_context.Produktet, "ProduktiId", "Emri", shitjaDto.ProduktiId);
            ViewData["Shitjet"] = _context.Shitjet.ToList();
            return View(shitjaDto);
        }

        public async Task<IActionResult> KrijoFaturen(string blersi,string destinacjoni)
        {
            var numriFatures = await MerrNumrinEFunditTeFaturesAsync();
            var shitjet = await _context.Shitjet.Include(s => s.Produkti)
                                                 .Where(s => s.NrFatures == numriFatures + 1)
                                                 .ToListAsync();

            double totali = 0;
            foreach (var shitja in shitjet)
            {
                if (shitja.Produkti != null)
                {
                    totali += shitja.Produkti.Cmimi * shitja.Sasia;
                }
            }

            // Krijo faturën e re
            var fatura = new Fatura
            {
                Blersi = blersi,
                Destinacjoni = destinacjoni,
                Totali = totali
            };


            foreach (var shitja in shitjet)
            {
                shitja.Blersi = blersi;
                _context.Update(shitja);
            }


            _context.Add(fatura);
            await _context.SaveChangesAsync();

            var numerFaturesPasKrijimit = numriFatures + 1;

            return RedirectToAction("Details", "Fatura", new { id = numerFaturesPasKrijimit });
        }

        private async Task<int> MerrNumrinEFunditTeFaturesAsync()
        {
            var numriFundit = await _context.Faturat.OrderByDescending(f => f.NrFatures).Select(f => f.NrFatures).FirstOrDefaultAsync();

            if (numriFundit == 0)
            {
                return 1;
            }
            return numriFundit;
        }

        // GET: Shitja/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Shitjet == null)
            {
                return NotFound();
            }

            var shitja = await _context.Shitjet.FindAsync(id);
            if (shitja == null)
            {
                return NotFound();
            }
            ViewData["ProduktiId"] = new SelectList(_context.Produktet, "ProduktiId", "ProduktiId", shitja.ProduktiId);
            return View(shitja);
        }

        // POST: Shitja/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProduktiId,Sasia,Blersi")] Shitja shitja)
        {
            if (id != shitja.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shitja);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShitjaExists(shitja.Id))
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
            ViewData["ProduktiId"] = new SelectList(_context.Produktet, "ProduktiId", "ProduktiId", shitja.ProduktiId);
            return View(shitja);
        }

        // GET: Shitja/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Shitjet == null)
            {
                return NotFound();
            }

            var shitja = await _context.Shitjet
                .Include(s => s.Produkti)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shitja == null)
            {
                return NotFound();
            }

            return View(shitja);
        }

        // POST: Shitja/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Shitjet == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Shitjet'  is null.");
            }
            var shitja = await _context.Shitjet.FindAsync(id);
            if (shitja != null)
            {
                _context.Shitjet.Remove(shitja);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Create", "Shitja");
        }

        private bool ShitjaExists(int id)
        {
          return (_context.Shitjet?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
