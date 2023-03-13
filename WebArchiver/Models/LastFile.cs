using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebArchiver.Models
{
    public class LastFile
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public double Size { get; set; }
        [Required]
        public String Name { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        [ForeignKey("User")]
        public int User_id { get; set; }
    }
}
