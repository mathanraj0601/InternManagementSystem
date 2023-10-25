using TicketAPI.Models;
using TicketAPI.Models.DTOs;

namespace TicketAPI.Interfaces
{
    public interface IFIlter
    {
        public Task<List<Ticket>?> GetAllTicketAndSolution();
        public Task<List<Ticket>?> GetAllTicketAndSolutionByDateandUser(TicketFilterDTO ticketFilterDTO);
        public Task<List<Ticket>?> GetAllUnAnsweredTicket();
        public Task<List<Ticket>?> GetAllUnAnsweredTicketByDateandUser(TicketFilterDTO ticketFilterDTO);


    }
}
