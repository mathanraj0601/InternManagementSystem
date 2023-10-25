using Microsoft.EntityFrameworkCore;
using TicketAPI.Exceptions;
using TicketAPI.Interfaces;
using TicketAPI.Models;

namespace TicketAPI.Services
{
    public class TicketRepoServices : IRepo<Ticket,int>
    {
        private readonly TicketContext _context;

        public TicketRepoServices(TicketContext context)
        {
            _context = context;
        }

        public async Task<Ticket?> Add(Ticket entity)
        {
            var ticket = await Get(entity.TicketID);
            if(ticket == null)
            {
                if(_context.Tickets != null)
                {
                    await _context.Tickets.AddAsync(entity);
                    await _context.SaveChangesAsync();
                    return entity;
                }
                throw new ContextException("Context is Empty");
            }
            return null;
        }

        public async Task<Ticket?> Delete(Ticket entity)
        {
            var ticket = await Get(entity.TicketID);
            if(ticket != null)
            {
                if(_context.Tickets != null)
                {
                    _context.Tickets.Remove(ticket);
                    await _context.SaveChangesAsync();
                    return ticket;
                }
                throw new ContextException("Context is Empty");
            }
            return null;
        }

        public async Task<Ticket?> Get(int id)
        {
            if(_context.Tickets!= null)
            {
                return await _context.Tickets.Include(l=>l.Solutions).FirstOrDefaultAsync(t=>t.TicketID == id);
            }
            throw new ContextException("Context is Empty");
        }

        public async Task<ICollection<Ticket>?> GetAll()
        {
            if(_context.Tickets != null)
            {
                return await _context.Tickets.Include(l => l.Solutions).ToListAsync();
            }
            throw new ContextException("Context is Empty");
        }


        public async Task<Ticket?> Update(Ticket entity)
        {
            var ticket = await Get(entity.TicketID);
            if(ticket != null)
            {
                if(_context.Tickets != null)
                {
                    ticket.IssueTitle = entity.IssueTitle;
                    ticket.IssueDetails = entity.IssueDetails;
                    ticket.IssuedDate = entity.IssuedDate;
                    _context.Tickets.Update(ticket);
                    await _context.SaveChangesAsync();
                    return ticket;
                }
                throw new ContextException("Context is Empty");
            } 
            return null;
        }
    }
}
