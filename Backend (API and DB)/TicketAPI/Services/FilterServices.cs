using System.Linq;
using TicketAPI.Interfaces;
using TicketAPI.Models;
using TicketAPI.Models.DTOs;

namespace TicketAPI.Services
{
    public class FilterServices : IFIlter
    {
        private readonly IRepo<Solution, int> _solutionRepo;
        private readonly IRepo<Ticket, int> _ticketRepo;

        public FilterServices(IRepo<Solution, int> solutionRepo, IRepo<Ticket,int> ticketRepo )
        {
            _solutionRepo = solutionRepo;
            _ticketRepo = ticketRepo;
        }

        public async Task<List<Ticket>?> GetAllTicketAndSolution()
        {
            var tickets = await _ticketRepo.GetAll();
            if(tickets != null)
                return tickets.ToList();
            return null;
        }


        public async Task<List<Ticket>?> GetAllTicketAndSolutionByDateandUser(TicketFilterDTO ticketFilterDTO)
        {
            var tickets = await GetAllTicketAndSolution();
            if(tickets != null)
            {
                if (ticketFilterDTO.UserID != null && ticketFilterDTO.Date != null)
                    return tickets.Where(t=>t.IssuedDate == ticketFilterDTO.Date.Value.Date && t.UserID == ticketFilterDTO.UserID).ToList();
                if(ticketFilterDTO.UserID != null)
                    return tickets.Where(t => t.UserID == ticketFilterDTO.UserID).ToList();
                if( ticketFilterDTO.Date != null)
                    return tickets.Where(t => t.IssuedDate == ticketFilterDTO.Date.Value.Date).ToList();
                return tickets.ToList();
            }
            return null;
        }



        public async Task<List<Ticket>?> GetAllUnAnsweredTicket()
        {
            var tickets = await _ticketRepo.GetAll();   
            if(tickets != null)
                return tickets.Where(t=> t.Solutions==null || t.Solutions.Count == 0).ToList();
            return null;

        }

        public async Task<List<Ticket>?> GetAllUnAnsweredTicketByDateandUser(TicketFilterDTO ticketFilterDTO)
        {
            var unAnsweredTickets = await GetAllUnAnsweredTicket();
            if(unAnsweredTickets != null)
            {
                if(ticketFilterDTO.Date!= null && ticketFilterDTO.UserID != null)
                {
                    return unAnsweredTickets.Where(t => t.IssuedDate.Date == ticketFilterDTO.Date.Value.Date && t.UserID == ticketFilterDTO.UserID).ToList();

                }
                if (ticketFilterDTO.Date != null)
                    return unAnsweredTickets.Where(t => t.IssuedDate.Date == ticketFilterDTO.Date.Value.Date).ToList();
                if(ticketFilterDTO.UserID!= null)
                    return unAnsweredTickets.Where(t=>t.UserID == ticketFilterDTO.UserID).ToList();
                return unAnsweredTickets.ToList();
            }
            return null;
        }

  

    }
}
