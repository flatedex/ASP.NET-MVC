using System.ComponentModel.DataAnnotations;

namespace WebArchiver.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String Email { get; set; }
        [Required]
        public String Password { get; set; }
    }
}
