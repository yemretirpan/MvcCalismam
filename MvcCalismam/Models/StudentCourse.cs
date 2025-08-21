namespace MvcCalismam.Models
{
    public class StudentCourse
    {
        public int StudentId { get; set; }
        public Ogrenci Student { get; set; } = null!;

        public int CourseId { get; set; }
        public Ders Course { get; set; } = null!;
    }
}
