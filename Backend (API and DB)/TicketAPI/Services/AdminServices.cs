using TicketAPI.Exceptions;
using TicketAPI.Interfaces;
using TicketAPI.Models;

namespace TicketAPI.Services
{
    public class AdminServices : IAdminAction
    {
        private readonly IRepo<Solution, int> _solutionRepo;
        private readonly IRepo<Ticket, int> _ticketRepo;

        public AdminServices(IRepo<Solution, int> solutionRepo, IRepo<Ticket,int> ticketRepo)
        {
            _solutionRepo = solutionRepo;
            _ticketRepo = ticketRepo;
        }

        public async Task<Solution?> AnswerTicket(Solution solution)
        {
            var ticket = await _ticketRepo.Get(solution.TicketID);
            if(ticket == null)
            {
                throw new TicketNotFoundException("Ticket not found");
            }
            else
            {
                solution.SolutionDate = solution.SolutionDate.Date;
                return await _solutionRepo.Add(solution);
            }
     
        }

        public async Task<Solution?> DeleteSolution(Solution solution)
        {

            return await _solutionRepo.Delete(solution);
        }

        public async Task<Solution?> EditSolution(Solution solution)
        {
            solution.SolutionDate = solution.SolutionDate.Date;
            return await _solutionRepo.Update(solution);
        }
    }
}
