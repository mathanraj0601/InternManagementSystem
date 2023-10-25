using UserAPI.Models;
using UserAPI.Models.DTOs;

namespace UserAPI.Interfaces
{
    public interface IAdminAction 
    {
        public Task<Intern?> ChangeInternStatus(User user);
        public Task<List<Intern>?> GetApprovedInternBasedOnStatus(InternFilterDTO internFilterDTO);
        public Task<List<Intern>?> GetAllIntern();

    }
}
