using Microsoft.Build.Framework;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;

namespace WebArchiver.Models
{
    public class RegisterModel
    {
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage= "Name is required")]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Confirmation is required")]
        [Compare("Password", ErrorMessage="Passwords are not matching")]
        public string PasswordConfirmed { get; set; }
    }
    public class LoginModel
    {
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
