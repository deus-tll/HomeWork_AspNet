using Library.Models.DataModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Library.Models.ContextModels
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Roles = new List<IdentityUserRole<string>>();
            CartItems = new List<CartItem>();
            Orders = new List<Order>();
        }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public DbSet<ProductDb> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
            Database.EnsureCreated();
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ProductDb>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            builder.Entity<ProductDb>()
                .HasMany(p => p.Images)
                .WithOne(pi => pi.Product)
                .HasForeignKey(pi => pi.ProductId);

            builder.Entity<ProductReview>()
                .HasOne(pr => pr.User)
                .WithMany()
                .HasForeignKey(pr => pr.UserId);

            builder.Entity<ProductReview>()
                .HasOne(pr => pr.Product)
                .WithMany()
                .HasForeignKey(pr => pr.ProductId);

            builder.Entity<ProductDb>()
            .Property(p => p.DateAdded)
            .HasColumnType("datetime");
        }
    }
}
