using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcCalismam.Data;
using MvcCalismam.Models;
using System.Linq;

namespace MvcCalismam.Controllers
{
    public class OgrencilerController : Controller
    {
        private readonly ApplicationDbContext _ctx;
        public OgrencilerController(ApplicationDbContext ctx) => _ctx = ctx;

        // GET: /Ogrenciler  ?search=...&yas=...
        public async Task<IActionResult> Index(string? search, int? yas)
        {
            var q = _ctx.Ogrenciler.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();
                q = q.Where(o =>
                    o.Ad.Contains(search) ||
                    o.Soyad.Contains(search) ||
                    o.Tc.Contains(search));
            }

            if (yas.HasValue)
                q = q.Where(o => o.Yasi == yas.Value);

            var list = await q
                .OrderBy(x => x.Ad).ThenBy(x => x.Soyad)
                .ToListAsync();

            ViewBag.Search = search;
            ViewBag.Yas = yas;
            return View(list);
        }

        [HttpGet]
        public IActionResult Create() => View(new Ogrenci());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ogrenci model)
        {
            if (!ModelState.IsValid) return View(model);
            _ctx.Ogrenciler.Add(model);
            await _ctx.SaveChangesAsync();
            TempData["ok"] = "Öğrenci başarıyla eklendi.";
            return RedirectToAction(nameof(Index));
        }

        // POST: /Ogrenciler/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string tc)
        {
            if (string.IsNullOrWhiteSpace(tc))
                return RedirectToAction(nameof(Index));

            var entity = await _ctx.Ogrenciler.FirstOrDefaultAsync(x => x.Tc == tc);
            if (entity != null)
            {
                _ctx.Ogrenciler.Remove(entity);
                await _ctx.SaveChangesAsync();
                TempData["ok"] = "Öğrenci silindi.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
