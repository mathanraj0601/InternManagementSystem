using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TicketAPI.Models
{
    public class Ticket
    {
        [Key]
        public int TicketID { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "User Id should be positive")]
        [DefaultValue(0)]
        public int UserID { get; set; }
        [Required]
        public string? IssueTitle { get; set; }
        [Required]
        public string? IssueDetails { get; set; }
        [Required]
        public DateTime IssuedDate { get; set; }
        public ICollection<Solution>? Solutions { get; set; }

    }
}
