using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcCalismam.Models
{
    public class Ogrenci
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "TC alanı zorunludur.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "TC 11 haneli olmalı.")]
        public string Tc { get; set; } = "";

        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Ad 2-50 karakter olmalı.")]
        public string Ad { get; set; } = "";

        [Required(ErrorMessage = "Soyad alanı zorunludur.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Soyad 2-50 karakter olmalı.")]
        public string Soyad { get; set; } = "";

        [Range(15, 100, ErrorMessage = "Yaş 15-100 arasında olmalı.")]
        public int Yasi { get; set; }

        // İlişki
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
