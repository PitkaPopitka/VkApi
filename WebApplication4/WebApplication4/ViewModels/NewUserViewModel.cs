using System.ComponentModel.DataAnnotations;

namespace WebApplication4.ViewModels
{
    public class NewUserViewModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Password mismatch")]
        public string ConfirmPassword { get; set;}
    }
}
