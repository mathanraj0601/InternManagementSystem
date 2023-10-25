using TicketAPI.Interfaces;
using TicketAPI.Models;

namespace TicketAPI.Services
{
    public class InternServices : IInternAction
    {
        private readonly IRepo<Ticket, int> _repo;

        public InternServices(IRepo<Ticket,int> repo)
        {
            _repo = repo;
        }
        public async Task<Ticket?> DeleteTicket(Ticket ticket)
        {
            return await _repo.Delete(ticket);
        }

        public async Task<Ticket?> EditTicket(Ticket ticket)
        {
            ticket.IssuedDate = ticket.IssuedDate.Date;
            return await _repo.Update(ticket);
        }

        public async Task<Ticket?> RaiseTicket(Ticket ticket)
        {
            ticket.IssuedDate = ticket.IssuedDate.Date;
            return await _repo.Add(ticket);
        }
    }
}
