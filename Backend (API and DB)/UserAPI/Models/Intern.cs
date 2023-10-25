using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserAPI.Models
{
    public class Intern
    {
        [Key]
        public int InternId { get; set; }
        [ForeignKey("InternId")]
        public User? User { get; set; }

        [Required]
        [MinLength(4, ErrorMessage = "Name should be minimum 4 character"), MaxLength(50, ErrorMessage = "Name should be less than 50 character")]
        [RegularExpression(@"^[A-Za-z. ]+$", ErrorMessage ="Name Should be Alphabetic")]
        public string? Name { get; set; }

        [Required]
        [Range(1,100,ErrorMessage = "Age should be between 1 to 100")]
        [DefaultValue(0)]
        public int Age { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage ="Email is not Valid")]
        public string? Email { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$",ErrorMessage ="Phone Number shoulb exact 10 digit")]
        public string? Phone { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [DefaultValue("Unknown")]
        public string? Gender { get; set; }

    }
}
