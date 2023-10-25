using LogAPI.Models;
using LogAPI.Models.DTOs;

namespace LogAPI.Interfaces
{
    public interface ILogAction
    {
        public Task<Log?> AddLog(Log log);
        public Task<Log?> EditLog(Log log);
        public Task<List<Log>?> GetAllLogsBasedOnUser(LogFilterDTO logFilterDTO);
        public Task<ICollection<Log>?> GetAllLogsBasedonUserAndDate(LogFilterDTO logFilterDTO);
        public Task<ICollection<Log>?> GetAllLog();

    }
}
