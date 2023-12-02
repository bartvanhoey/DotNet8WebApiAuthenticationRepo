using System.ComponentModel.DataAnnotations;

namespace DotNet8BlazorWebApp.Authentication
{
    public class LoginModel
    {
        // [Required]
        // public string Email { get; set; }

        // [Required]
        // public string Password { get; set; }

        public bool RememberMe { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

    }
}