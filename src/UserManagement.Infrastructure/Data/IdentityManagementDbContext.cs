using IdentityManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
