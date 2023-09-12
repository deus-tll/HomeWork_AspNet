using InternetShop.Models.DataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InternetShop.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Roles = new List<IdentityUserRole<string>>();
        }

        public int YearOfBirth { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }
    }


    public class ApplicationContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Message> Messages { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}
