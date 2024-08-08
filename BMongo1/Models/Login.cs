using System.ComponentModel.DataAnnotations;

namespace BMongo1.Models
{
    public class Login
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email cannot be blank")]
        [EmailAddress(ErrorMessage = "Invalid email.")]
        public string? Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password can not be blank")]
        public string? Password { get; set; }
    }
}
