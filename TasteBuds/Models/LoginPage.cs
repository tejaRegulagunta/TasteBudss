using System.ComponentModel.DataAnnotations;

namespace TasteBuds.Models
{
    public class LoginPage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Email { get; set;}


        public string? Password { get; set;}

       


    }
}
