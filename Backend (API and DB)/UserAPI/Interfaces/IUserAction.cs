using UserAPI.Models.DTOs;
using UserAPI.Models;

namespace UserAPI.Interfaces
{
    public interface IUserAction : IAdminAction, IInternAction
    {
        public Task<User?> UpdatePassword(UserDTO userDTO);
        public Task<UserDTO?> Register(InternDTO internDTO);
    }
}
