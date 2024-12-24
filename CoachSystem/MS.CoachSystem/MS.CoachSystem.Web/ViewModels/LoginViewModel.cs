
using System.ComponentModel.DataAnnotations;

namespace MS.CoachSystem.Web.ViewModels
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
