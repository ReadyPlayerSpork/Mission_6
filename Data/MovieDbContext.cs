using Microsoft.EntityFrameworkCore;
using Mission_6.Models;

namespace Mission_6.Data;

public class MovieDbContext : DbContext
{
    public MovieDbContext(DbContextOptions<MovieDbContext> options)
        : base(options)
    {
    }

    public DbSet<Movie> Movies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Category).HasMaxLength(100);
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.Director).HasMaxLength(200);
            entity.Property(e => e.Rating).HasMaxLength(10);
            entity.Property(e => e.LentTo).HasMaxLength(100);
            entity.Property(e => e.Notes).HasMaxLength(25);
        });
    }
}
