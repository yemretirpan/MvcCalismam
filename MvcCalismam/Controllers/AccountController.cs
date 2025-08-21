using Microsoft.AspNetCore.Mvc;
using MvcCalismam.ViewModels;

namespace MvcCalismam.Controllers
{
    public class AccountController : Controller
    {
        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login() => View(new LoginViewModel());

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            // Şimdilik gerçek kimlik doğrulama yok; sadece demo akışı
            TempData["ok"] = "Giriş başarılı (demo).";
            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register() => View(new RegisterViewModel());

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            // Şimdilik veri tabanına kayıt yok; sadece demo akışı
            TempData["ok"] = "Kayıt başarılı (demo).";
            return RedirectToAction("Login");
        }

        // /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            TempData["ok"] = "Çıkış yapıldı (demo).";
            return RedirectToAction("Index", "Home");
        }
    }
}
