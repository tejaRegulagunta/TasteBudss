using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TasteBuds.Models
{
    public class RegisterPage :IdentityUser
    {
        [Key]
        public int? Id { get; set; }

        [Required(ErrorMessage = "Enter Name Is Mandatory")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Provide the Valid the EmailAddress ,Use this reference Teja@123.com")]
        [EmailAddress]
        public override string? Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Provide The Secure Password")]
        [Compare("Password")]
        public string? Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "This Password has to match the previous one")]
        [Compare("ConfirmPassword")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public string? Role { get; set; } //Default
    }
   
}
