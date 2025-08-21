using System.Linq;
using MvcCalismam.Data;
using MvcCalismam.Models;

namespace MvcCalismam
{
    public static class Seed
    {
        public static void EnsureSeeded(ApplicationDbContext ctx)
        {
            ctx.Database.EnsureCreated();

            if (!ctx.Dersler.Any())
            {
                ctx.Dersler.AddRange(
                    new Ders { Kod = "MAT101", Ad = "Matematik I", Kredi = 6 },
                    new Ders { Kod = "FIZ101", Ad = "Fizik I", Kredi = 5 },
                    new Ders { Kod = "YAZ101", Ad = "Yazılım Giriş", Kredi = 4 }
                );
                ctx.SaveChanges();
            }
        }
    }
}
