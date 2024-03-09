using KULLA.Data;
using KULLA.Models.dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KULLA.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var shumaTotale = _context.Faturat.Sum(s => s.Totali);

            var shumaBesnik = _context.Faturat.Where(f => f.User.Ortak == "Besnik").Sum(s => s.Totali);
            var shumaShkelzenit = _context.Faturat.Where(f => f.User.Ortak == "Shkelzen").Sum(s => s.Totali);

            var faturat = await _context.Faturat.ToListAsync();

            foreach (var fatura in faturat)
            {
                var shitesiId = fatura.Shitesi;
                var shitesi = await _context.Users.FirstOrDefaultAsync(u => u.Id == shitesiId);
                ViewData[$"ShitesiName{fatura.NrFatures}"] = shitesi?.FullName;
            }

            var dashboard = new Dashboard
            {
                ShitjaTotale = shumaTotale,
                ShitjaBesnikit = shumaBesnik,
                ShitjaShkelezenit = shumaShkelzenit,
                Faturat = faturat
            };

            return View(dashboard);
        }
    }
}
