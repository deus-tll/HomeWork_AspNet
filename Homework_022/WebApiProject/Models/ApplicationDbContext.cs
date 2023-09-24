using Microsoft.EntityFrameworkCore;

namespace WebApiProject.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
