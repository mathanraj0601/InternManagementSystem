using LogAPI.Exceptions;
using LogAPI.Interfaces;
using LogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LogAPI.Services
{
    public class LogRepoServices : IRepo
    {
        private readonly LogContext _context;

        public LogRepoServices(LogContext context)
        {
            _context = context;
        }

        public async Task<Log?> Add(Log log)
        {
            Log? existingLog = await Get(log.LogId);
            if (existingLog == null)
            {
                if (_context.Logs != null)
                {
                    await _context.Logs.AddAsync(log);
                    await _context.SaveChangesAsync();
                    return log;
                }
                throw new ContextException("Context is empty");
               
            }
            return null;
        }

        public async Task<Log?> Delete(Log log)
        {
            Log? existingLog = await Get(log.LogId);
            if (existingLog != null)
            {
                if(_context.Logs != null)
                {
                    _context.Logs.Remove(log);
                    await _context.SaveChangesAsync();
                    return existingLog;
                }
                throw new ContextException("Context is empty");
            }
            return null;

        }

        public async Task<Log?> Get(int id)
        {
            if(_context.Logs != null)
            {
               return await _context.Logs.FirstOrDefaultAsync(l => l.LogId == id);   
            }
            throw new ContextException("Context is empty");
        }

        public async Task<ICollection<Log>?> GetAll()
        {
            if(_context.Logs != null)
            {
                return await _context.Logs.ToListAsync();
            }
            return null;
        }

        public async Task<Log?> Update(Log log)
        {
            Log? existingLog = await Get(log.LogId);
            if (existingLog != null)
            {
                if(_context.Logs != null)
                {
                    existingLog.LogOutTime = log.LogOutTime;
                    _context.Logs.Update(existingLog);
                    await _context.SaveChangesAsync();
                    return existingLog;
                }
                throw new ContextException("Context is empty");
            }
            return null;
        }
    }
}
