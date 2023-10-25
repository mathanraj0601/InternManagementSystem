using UserAPI.Models.DTOs;
using UserAPI.Models;

namespace UserAPI.Interfaces
{
    public interface IInternAction
    {
        public Task<UserDTO?> Login(UserDTO userDTO);
      
        
    }
}
