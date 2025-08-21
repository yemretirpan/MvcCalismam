using System.ComponentModel.DataAnnotations;

namespace MvcCalismam.Models
{
    public class Enrollment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int StudentId { get; set; }          // Foreign key -> Ogrenci.Id
        public Ogrenci Student { get; set; } = null!;

        [Required]
        public int CourseId { get; set; }           // Foreign key -> Ders.Id
        public Ders Course { get; set; } = null!;

        [Range(0, 100, ErrorMessage = "Not 0-100 arasında olmalı.")]
        public decimal? NotDegeri { get; set; }
    }
}
