using Microsoft.EntityFrameworkCore;

namespace TicketAPI.Models
{
    public class TicketContext: DbContext
    {
        public TicketContext(DbContextOptions<TicketContext> options):base(options) { }

        public DbSet<Ticket>? Tickets { get; set; }
        public DbSet<Solution>? Solutions { get; set; }

    }
}
