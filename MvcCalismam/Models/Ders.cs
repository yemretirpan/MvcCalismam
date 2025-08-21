using System.ComponentModel.DataAnnotations;

namespace MvcCalismam.Models
{
    public class Ders
    {
        [Key]
        public int Id { get; set; }

        [Required, RegularExpression(@"^[A-Z]{3}\d{3}$", ErrorMessage = "Kod örnek: MAT101")]
        public string Kod { get; set; } = "";

        [Required, StringLength(80, MinimumLength = 2)]
        public string Ad { get; set; } = "";

        [Range(1, 10, ErrorMessage = "Kredi 1-10 arasında olmalı.")]
        public int Kredi { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
