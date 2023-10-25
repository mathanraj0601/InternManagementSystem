using UserAPI.Models.DTOs;

namespace UserAPI.Interfaces
{
    public interface IGeneratePassword
    {   
        public string GeneratePassword(InternDTO internDTO);
    }
}
