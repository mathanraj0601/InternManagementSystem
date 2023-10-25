using TicketAPI.Models;

namespace TicketAPI.Interfaces
{
    public interface IAdminAction
    {
        public Task<Solution?> AnswerTicket(Solution solution);
        public Task<Solution?> DeleteSolution(Solution solution);
        public Task<Solution?> EditSolution(Solution solution);

    }

}
