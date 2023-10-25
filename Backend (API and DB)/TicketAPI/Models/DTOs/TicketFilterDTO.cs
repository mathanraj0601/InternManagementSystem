namespace TicketAPI.Models.DTOs
{
    public class TicketFilterDTO
    {
        public int? UserID { get; set; }
        public int? TicketID { get; set; }
        public DateTime? Date { get; set; }
    }
}
