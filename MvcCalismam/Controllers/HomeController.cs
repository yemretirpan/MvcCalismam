using Microsoft.AspNetCore.Mvc;
using MvcCalismam.Data;

namespace MvcCalismam.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _ctx;
        public HomeController(ApplicationDbContext ctx) => _ctx = ctx;

        public IActionResult Index()
        {
            ViewBag.OgrenciSayisi = _ctx.Ogrenciler.Count();
            ViewBag.DersSayisi = _ctx.Dersler.Count();
            ViewBag.DanismanSayisi = 0; // Danýþman tablon yoksa þimdilik 0
            return View();
        }

        public IActionResult Privacy() => View();
        public IActionResult Error() => View();
    }
}
