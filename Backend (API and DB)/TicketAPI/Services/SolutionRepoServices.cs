using Microsoft.EntityFrameworkCore;
using TicketAPI.Exceptions;
using TicketAPI.Interfaces;
using TicketAPI.Models;

namespace TicketAPI.Services
{
    public class SolutionRepoServices : IRepo<Solution, int>
    {
        private readonly TicketContext _context;

        public SolutionRepoServices(TicketContext context)
        {
            _context = context;
        }
        public async Task<Solution?> Add(Solution entity)
        {
            var solution = await Get(entity.SolutionID);
            if(solution == null)
            {
                if(_context.Solutions!= null)
                {
                    await _context.Solutions.AddAsync(entity);
                    await _context.SaveChangesAsync();
                    return entity;
                }
                throw new ContextException("Context is Empty");
            }
            return null;
        }

        public async Task<Solution?> Delete(Solution entity)
        {
            var solution = await Get(entity.SolutionID);
            if(solution != null)
            {
                if(_context.Solutions != null) { 
                    _context.Solutions.Remove(solution);
                    await _context.SaveChangesAsync();
                    return solution;
                }
                throw new ContextException("Context is Empty");
            }
            return null;
        }

        public async Task<Solution?> Get(int id)
        {
            if(_context.Solutions != null)
            {
                return await _context.Solutions.FirstOrDefaultAsync(s=>s.SolutionID == id);
            }
            throw new ContextException("Context is Empty");
        }

        public async Task<ICollection<Solution>?> GetAll()
        {
            if(_context.Solutions != null)
            {
                return await _context.Solutions.ToListAsync();
            }
            throw new ContextException("Context is Empty");
        }

        public async Task<Solution?> Update(Solution entity)
        {
            var existingSolution = await Get(entity.SolutionID);
            if(existingSolution != null)
            {
                if(_context.Solutions != null)
                {
                    existingSolution.SolutionProvided = entity.SolutionProvided;
                    existingSolution.SolutionDate = entity.SolutionDate;
                    _context.Solutions.Update(existingSolution);
                    await _context.SaveChangesAsync();
                    return entity;
                }
                throw new ContextException("Context is Empty");
            }
            return null;
        }
    }
}
