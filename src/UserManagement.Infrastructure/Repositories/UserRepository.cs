using IdentityManagement.Domain.Entities;
using IdentityManagement.Domain.Interfaces;
using IdentityManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityManagement.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IdentityManagementDbContext _context;

        public UserRepository(IdentityManagementDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<User> GetUserByUserNameAsync(string userName, CancellationToken cancellationToken)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _context.Users.SingleOrDefaultAsync(u => u.UserName.Equals(userName), cancellationToken);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<bool> IsUserNameUniqueAsync(string userName, CancellationToken cancellationToken)
        {
            return await _context.Users.AllAsync(u => u.UserName != userName, cancellationToken);
        }

        public async Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken)
        {
            return await _context.Users.AllAsync(u => u.Email != email, cancellationToken);
        }
    }
}
