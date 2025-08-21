using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using MvcCalismam.Data;
using MvcCalismam.Models;

namespace MvcCalismam.Controllers
{
    public class KayitlarController : Controller
    {
        private readonly ApplicationDbContext _ctx;
        public KayitlarController(ApplicationDbContext ctx) => _ctx = ctx;

        // GET: /Kayitlar
        public async Task<IActionResult> Index(int? studentId, int? courseId)
        {
            var q = _ctx.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .AsQueryable();

            if (studentId.HasValue) q = q.Where(e => e.StudentId == studentId.Value);
            if (courseId.HasValue) q = q.Where(e => e.CourseId == courseId.Value);

            ViewBag.Students = new SelectList(await _ctx.Ogrenciler
                .OrderBy(x => x.Ad).ThenBy(x => x.Soyad).ToListAsync(), "Id", "Ad");
            ViewBag.Courses = new SelectList(await _ctx.Dersler
                .OrderBy(x => x.Kod).ToListAsync(), "Id", "Kod");

            var list = await q.OrderBy(e => e.Student.Ad).ThenBy(e => e.Course.Kod).ToListAsync();
            return View(list);
        }

        // GET: /Kayitlar/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await FillSelects();
            return View(new Enrollment());
        }

        // POST: /Kayitlar/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int studentId, int courseId, string? notDegeri)
        {
            decimal? parsed = null;
            if (!string.IsNullOrWhiteSpace(notDegeri))
            {
                // TR ve invariant dene
                if (!(decimal.TryParse(notDegeri, NumberStyles.Any, CultureInfo.CurrentCulture, out var d) ||
                      decimal.TryParse(notDegeri, NumberStyles.Any, CultureInfo.InvariantCulture, out d)))
                {
                    ModelState.AddModelError("NotDegeri", "Not değeri sayısal olmalıdır.");
                }
                else parsed = d;
            }

            // Aynı öğrenci+ders için tekrar kayıt engeli
            var exists = await _ctx.Enrollments.AnyAsync(e => e.StudentId == studentId && e.CourseId == courseId);
            if (exists)
                ModelState.AddModelError("", "Bu öğrenci bu derse zaten kayıtlı.");

            if (!ModelState.IsValid)
            {
                await FillSelects();
                return View(new Enrollment { StudentId = studentId, CourseId = courseId, NotDegeri = parsed });
            }

            _ctx.Enrollments.Add(new Enrollment
            {
                StudentId = studentId,
                CourseId = courseId,
                NotDegeri = parsed
            });
            await _ctx.SaveChangesAsync();
            TempData["ok"] = "Kayıt eklendi.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Kayitlar/Edit/5  (Sadece Not günceller)
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var e = await _ctx.Enrollments
                .Include(x => x.Student)
                .Include(x => x.Course)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (e == null) return NotFound();
            return View(e);
        }

        // POST: /Kayitlar/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string? notDegeri)
        {
            var e = await _ctx.Enrollments.FindAsync(id);
            if (e == null) return NotFound();

            decimal? parsed = null;
            if (!string.IsNullOrWhiteSpace(notDegeri))
            {
                if (!(decimal.TryParse(notDegeri, NumberStyles.Any, CultureInfo.CurrentCulture, out var d) ||
                      decimal.TryParse(notDegeri, NumberStyles.Any, CultureInfo.InvariantCulture, out d)))
                {
                    ModelState.AddModelError("NotDegeri", "Not değeri sayısal olmalıdır.");
                }
                else parsed = d;
            }

            if (!ModelState.IsValid)
            {
                // Görünüm için ilişki bilgilerini tekrar doldur
                e = await _ctx.Enrollments
                    .Include(x => x.Student)
                    .Include(x => x.Course)
                    .FirstAsync(x => x.Id == id);
                return View(e);
            }

            e.NotDegeri = parsed;
            await _ctx.SaveChangesAsync();
            TempData["ok"] = "Not güncellendi.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Kayitlar/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var e = await _ctx.Enrollments
                .Include(x => x.Student)
                .Include(x => x.Course)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (e == null) return NotFound();
            return View(e);
        }

        // POST: /Kayitlar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var e = await _ctx.Enrollments.FindAsync(id);
            if (e == null) return NotFound();

            _ctx.Enrollments.Remove(e);
            await _ctx.SaveChangesAsync();
            TempData["ok"] = "Kayıt silindi.";
            return RedirectToAction(nameof(Index));
        }

        private async Task FillSelects()
        {
            ViewBag.Students = new SelectList(
                await _ctx.Ogrenciler.OrderBy(o => o.Ad).ThenBy(o => o.Soyad).ToListAsync(),
                "Id", "Ad");
            ViewBag.Courses = new SelectList(
                await _ctx.Dersler.OrderBy(c => c.Kod).ToListAsync(),
                "Id", "Kod");
        }
    }
}
