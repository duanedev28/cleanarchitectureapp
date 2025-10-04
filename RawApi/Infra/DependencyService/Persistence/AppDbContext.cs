using Microsoft.EntityFrameworkCore;
using RawApi.Domain.Entities;

namespace RawApi.Infra
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ===== User Entity =====
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(u => u.Id);
                entity.HasIndex(u => u.Name).IsUnique();
                entity.Property(u => u.Name).IsRequired().HasMaxLength(100);
                entity.Property(u => u.PasswordHash).IsRequired();
                entity.Property(u => u.Role).HasMaxLength(50).HasDefaultValue("User");
                entity.Property(u => u.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });

            // ===== Product Entity =====
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(200);
                entity.Property(p => p.Description).HasMaxLength(1000);
                entity.Property(p => p.Price).HasColumnType("decimal(18,2)");
            });

            base.OnModelCreating(modelBuilder);
        }
    }

}