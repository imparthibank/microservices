using IdentityManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityManagement.Infrastructure.Data
{
    public class IdentityManagementDbContext : DbContext
    {
        public IdentityManagementDbContext(DbContextOptions<IdentityManagementDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
