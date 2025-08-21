using System.ComponentModel.DataAnnotations;

namespace MvcCalismam.ViewModels
{
    public class RegisterViewModel
    {
        [Required, StringLength(50)]
        public string Ad { get; set; } = "";

        [Required, StringLength(50)]
        public string Soyad { get; set; } = "";

        [Required, EmailAddress]
        public string Email { get; set; } = "";

        [Required, DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = "";

        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = "";
    }
}
