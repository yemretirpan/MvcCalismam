using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcCalismam.Data;
using MvcCalismam.Models;
using System.Linq;

namespace MvcCalismam.Controllers
{
    public class DerslerController : Controller
    {
        private readonly ApplicationDbContext _ctx;
        public DerslerController(ApplicationDbContext ctx) => _ctx = ctx;

        // GET: /Dersler  ?q=...&kredi=...
        public async Task<IActionResult> Index(string? q, int? kredi)
        {
            var query = _ctx.Dersler.AsQueryable(); // Course DbSet adı sende farklıysa düzelt

            if (!string.IsNullOrWhiteSpace(q))
            {
                q = q.Trim();
                query = query.Where(d => d.Kod.Contains(q) || d.Ad.Contains(q));
            }

            if (kredi.HasValue)
                query = query.Where(d => d.Kredi == kredi.Value);

            var list = await query
                .OrderBy(d => d.Kod)
                .ToListAsync();

            ViewBag.Q = q;
            ViewBag.Kredi = kredi;
            return View(list);
        }

        [HttpGet]
        public IActionResult Create() => View(new Ders());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ders model)
        {
            if (!ModelState.IsValid) return View(model);
            _ctx.Dersler.Add(model);
            await _ctx.SaveChangesAsync();
            TempData["ok"] = "Ders eklendi.";
            return RedirectToAction(nameof(Index));
        }

        // POST: /Dersler/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string kod)
        {
            if (string.IsNullOrWhiteSpace(kod))
                return RedirectToAction(nameof(Index));

            var entity = await _ctx.Dersler.FirstOrDefaultAsync(x => x.Kod == kod);
            if (entity != null)
            {
                _ctx.Dersler.Remove(entity);
                await _ctx.SaveChangesAsync();
                TempData["ok"] = "Ders silindi.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
