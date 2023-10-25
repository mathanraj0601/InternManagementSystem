using LogAPI.Exceptions;
using LogAPI.Interfaces;
using LogAPI.Models;
using LogAPI.Models.DTOs;

namespace LogAPI.Services
{
    public class LogServices : ILogAction
    {
        private readonly IRepo _repo;

        public LogServices(IRepo repo)
        {
            _repo = repo;
        }

        public async Task<Log?> AddLog(Log log)
        {
            log.Date = log.Date.Date;
            if(log.LogInTime.TimeOfDay > log.LogOutTime.TimeOfDay)
            {
                throw new LogException("Log out time cannot be less than log in time");
            }
            return await _repo.Add(log);
        }

        public async Task<Log?> EditLog(Log log)
        {
            log.Date = log.Date.Date;
            if (log.LogInTime.TimeOfDay > log.LogInTime.TimeOfDay)
            {
                throw new LogException("Log out time cannot be less than log in time");
            }
            return await _repo.Update(log);
        }

        public async Task<ICollection<Log>?> GetAllLog()
        {
            var logs = await _repo.GetAll();
           
            if(logs != null)
            {
                //List<int> users = logs.Select(l => l.UserID).Distinct().ToList();
                //List<Log> result = new List<Log>();
                //foreach(var user in users)
                //{
                //    var existingLogs =  logs.Where(l=>l.UserID == user).ToList();
                //    if(existingLogs.Count ==1)
                //        result.Add(existingLogs[0]);
                //    else
                //        result.Add(mergeInterval(existingLogs));
                //}
                return logs.OrderBy(l=>l.UserID).ToList();
            }
            return null;
        }

        public async Task<ICollection<Log>?> GetAllLogsBasedonUserAndDate(LogFilterDTO logFilterDTO)
        {
            logFilterDTO.Date = logFilterDTO.Date.Date;
            var logs = await _repo.GetAll();
            if(logs != null)
            {
                HashSet<Log> result = new HashSet<Log>();
                    var logsBasedOnDate = (logs).Where(l => l.UserID == logFilterDTO.UserID && l.Date == logFilterDTO.Date).OrderBy(l => l.LogOutTime).ToList();
                    if (logsBasedOnDate.Count == 1)
                    {
                        result.Add(logsBasedOnDate[0]);
                    }
                    else if (logsBasedOnDate.Count > 1)
                    {
                        result.Add(mergeInterval(logsBasedOnDate));
                    }
                return result;
            }
            return null;
        }


        

        public async Task<List<Log>?> GetAllLogsBasedOnUser(LogFilterDTO logFilterDTO)
        {
            var logs= await _repo.GetAll();
            if(logs != null)
            {
                var result =  logs.Where(l => l.UserID == logFilterDTO.UserID).ToList();
                return result;
            }
            return null;
        }

        private Log mergeInterval(IList<Log> logs)
        {
             return new Log { UserID = logs[0].UserID, Date = logs[0].Date, LogId = logs[0].LogId, LogInTime = logs[0].LogInTime, LogOutTime = logs[logs.Count - 1].LogOutTime };
            
        }
    }
}
