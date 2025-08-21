using Microsoft.EntityFrameworkCore;
using MvcCalismam.Models;

namespace MvcCalismam.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Ogrenci> Ogrenciler { get; set; } = null!;
        public DbSet<Ders> Dersler { get; set; } = null!;
        public DbSet<Enrollment> Enrollments { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --- UNIQUE INDEXLER ---
            modelBuilder.Entity<Ogrenci>()
                .HasIndex(x => x.Tc)
                .IsUnique();

            modelBuilder.Entity<Ders>()
                .HasIndex(x => x.Kod)
                .IsUnique();

            modelBuilder.Entity<Enrollment>()
                .HasIndex(x => new { x.StudentId, x.CourseId })
                .IsUnique();

            // --- İLİŞKİLER + SİLME DAVRANIŞI ---
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
