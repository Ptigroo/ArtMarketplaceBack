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

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Artisan)
                .WithMany(u => u.ArtisanProducts).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Buyer)
                .WithMany(u => u.BuyerProducts).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
