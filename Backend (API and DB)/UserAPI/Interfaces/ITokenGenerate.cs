using UserAPI.Models;
using UserAPI.Models.DTOs;

namespace UserAPI.Interfaces
{
    public interface ITokenGenerate
    {
        public Task<string> GenerateJSONWebToken(UserDTO user);
    }
}
