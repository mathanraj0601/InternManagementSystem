using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TicketAPI.Models
{
    public class Solution
    {
        [Key]
        public int SolutionID { get; set; }

        [Required]
        [Range(0, int.MaxValue,ErrorMessage ="Ticket Id should be positive")]
        [DefaultValue(0)]
        public int TicketID { get; set; }
        [ForeignKey("TicketID")]
        [JsonIgnore]
        public Ticket? Ticket { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "User Id should be positive")]
        [DefaultValue(0)]
        public int UserID { get; set; }

        [Required]
        public string? SolutionProvided { get; set; }

        [Required]
        public DateTime SolutionDate { get; set; }

    }
}
