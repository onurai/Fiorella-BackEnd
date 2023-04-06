using Fiorella.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace Fiorella.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Picture> Pictures { get; set; }
        public DbSet<User> Users { get; set; }  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Picture>(b =>
            {
                b.ToTable("Picture");
                b.HasKey(k => k.Id);
                b.Property(q => q.Name).HasMaxLength(20).HasColumnName("Name");
                b.Property(l => l.Description).HasMaxLength(20).HasColumnName("Description");
                b.Property(l => l.Category).HasMaxLength(20).HasColumnName("Category");
                b.Property(l => l.Price).HasColumnName("Price");
                b.Property(l => l.Source).HasColumnName("Source");
                b.HasIndex(i => i.Source).IsUnique();
            });
        }
    }
}
