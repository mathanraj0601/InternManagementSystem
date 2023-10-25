using Microsoft.EntityFrameworkCore;
using UserAPI.Exceptions;
using UserAPI.Interfaces;
using UserAPI.Models;

namespace UserAPI.Services
{
    public class UserRepoService : IRepo<User, int>
    {
        private readonly UserContext _context;

        public UserRepoService(UserContext context)
        {
            _context = context;
        }

        public async Task<User?> Add(User entity)
        {
            var user = await Get(entity.Id);
            if (user == null)
            {
                if (_context.Users != null)
                {
                    await _context.Users.AddAsync(entity);
                    await _context.SaveChangesAsync();
                    return entity;
                }
                else
                    throw new ContextException("Context is Empty");
            }
            return null;
        }

        public async Task<User?> Delete(User entity)
        {
            var user = await Get(entity.Id);
            if (user != null)
            {
                if(_context.Users != null)
                {
                    _context.Users.Remove(entity);
                    await _context.SaveChangesAsync();
                }
                else
                    throw new ContextException("Context is Empty");
            }
            return user;
        }

        public async Task<User?> Get(int id)
        {
            if(_context.Users != null)
            {
                return await _context.Users.FirstOrDefaultAsync(u=>u.Id == id);
            }
            throw new ContextException("Context is Empty");
        }

        public async Task<ICollection<User>?> GetAll()
        {
            if (_context.Users != null)
            {
                return await _context.Users.ToListAsync<User>();
            }
            throw new ContextException("Context is Empty");
        }

        public async Task<User?> Update(User entity)
        {
            var user = await Get(entity.Id);
            if(user != null)
            {
                if(_context.Users != null)
                {
                    user.PasswordKey = entity.PasswordKey;
                    user.PasswordHash = entity.PasswordHash;
                    user.Role = entity.Role;
                    _context.Users.Update(entity);
                    await _context.SaveChangesAsync();
                }
                else
                    throw new ContextException("Context is Empty");
            }
            return user;
        }
    }
}
