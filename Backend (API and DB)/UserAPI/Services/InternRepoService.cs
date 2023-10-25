using Microsoft.EntityFrameworkCore;
using UserAPI.Exceptions;
using UserAPI.Interfaces;
using UserAPI.Models;

namespace UserAPI.Services
{
    public class InternRepoService : IRepo<Intern, int>
    {
        private readonly UserContext _context;

        public InternRepoService(UserContext context)
        {
            _context = context;
        }
        public async Task<Intern?> Add(Intern entity)
        {
            var intern = await Get(entity.InternId);
            if(intern == null)
            {
                if(_context.Interns != null)
                {
                    await _context.Interns.AddAsync(entity);
                    await _context.SaveChangesAsync();
                    return entity;
                }
                else
                    throw new ContextException("Context is Empty");
            }
            return null;

        }

        public async Task<Intern?> Delete(Intern entity)
        {
            var intern = await Get(entity.InternId);
            if(intern != null)
            {
                if(_context.Interns != null)
                {
                    _context.Interns.Remove(entity);
                    await _context.SaveChangesAsync();
                }
                else
                    throw new ContextException("Context is Empty");
            }
            return intern;

        }

        public async Task<Intern?> Get(int id)
        {
            if(_context.Interns!= null)
            {
                return await _context.Interns.Include(U=>U.User).FirstOrDefaultAsync(i=>i.InternId == id);
            }
            throw new ContextException("Context is empty");
        }

        public async Task<ICollection<Intern>?> GetAll()
        {
            if(_context.Interns != null)
            {
                return await _context.Interns.Include(i=>i.User).ToListAsync();
            }
            throw new ContextException("Context is empty");
        }

        public async Task<Intern?> Update(Intern entity)
        {
            var intern = await Get(entity.InternId);
            if(intern != null)
            {
                if(_context.Interns != null)
                {
                    intern.Name = entity.Name;
                    intern.Email = entity.Email;
                    intern.Phone = entity.Phone;
                    intern.Email = entity.Email;
                    intern.DateOfBirth = entity.DateOfBirth;
                    intern.Age = entity.Age;
                    intern.Gender = entity.Gender;
                    if (intern.User != null && entity.User != null)
                    {
                        intern.User.PasswordKey = entity.User.PasswordKey;
                        intern.User.PasswordHash = entity.User.PasswordHash;
                        intern.User.Role = entity.User.Role;
                    }
                 
                    _context.Interns.Update(intern);
                    await _context.SaveChangesAsync();
                    return intern;
                }
                else
                     throw new ContextException("Context is Empty");
            }
            return null;
        }
    }
}
