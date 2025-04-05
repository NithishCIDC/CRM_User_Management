using CRM_User.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace CRM_User.infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Organization> Organization { get; set; }
    }
}
