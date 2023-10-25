using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UserAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordKey { get; set; }

        [DefaultValue(false)]
        public bool Status { get; set; }

        [DefaultValue("Intern")]
        public string? Role { get; set; }

    }
}
