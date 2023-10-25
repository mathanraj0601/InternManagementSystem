using TicketAPI.Models;

namespace TicketAPI.Interfaces
{
    public interface IInternAction
    {
        public Task<Ticket?> RaiseTicket(Ticket ticket);
        public Task<Ticket?> EditTicket(Ticket ticket);
        public Task<Ticket?> DeleteTicket(Ticket ticket);


    }
}
