using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Entities;

namespace Portfolio.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Technology> Technology { get; set; }

    public DbSet<Project> Projects { get; set; }

    public DbSet<WebsiteConfig> WebsiteConfig { get; set; }

    public DbSet<BlogPost> BlogPosts { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>()
            .HasMany(t => t.Technologies)
            .WithMany(d => d.Projects)
            .UsingEntity(j => j.ToTable("TechnologyProjects"));

        modelBuilder.Entity<WebsiteConfig>()
            .HasMany(t => t.Technologies)
            .WithOne(d => d.WebsiteConfig)
            .HasForeignKey(z => z.WebsiteConfigId);
    }
}
