using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Entities;

namespace Portfolio.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Technology> Technology { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
