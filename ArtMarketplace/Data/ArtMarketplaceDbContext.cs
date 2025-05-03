using ArtMarketplace.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ArtMarketplace.Data
{
    public class ArtMarketplaceDbContext : DbContext
    {
        public ArtMarketplaceDbContext(DbContextOptions<ArtMarketplaceDbContext> options)
            : base(options)
        {
        }

        // Tables
        public DbSet<User> Users { get; set; } = null!;
        /*public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;
        public DbSet<Delivery> Deliveries { get; set; } = null!;*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Par exemple, contrainte unique sur Email
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Configurations supplémentaires (relations, tailles, etc.)
        }
    }
}
