    using UserAPI.Interfaces;
using UserAPI.Models.DTOs;

namespace UserAPI.Services
{
    public class GeneratePasswordService : IGeneratePassword
    {
        public string GeneratePassword(InternDTO internDTO)
        {
            if(internDTO != null)
            {
                return internDTO.Name?.Substring(0,4)+internDTO.DateOfBirth.Day.ToString()+internDTO.DateOfBirth.Month.ToString();
            }
            return "";
        }
    }
}
