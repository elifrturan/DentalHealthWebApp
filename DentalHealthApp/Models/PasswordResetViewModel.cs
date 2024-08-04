using System.ComponentModel.DataAnnotations;

namespace DentalHealthApp.Models
{
    public class PasswordResetViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Şifreler eşleşmiyor.")]
        public string ConfirmPassword { get; set; }
    }
}
