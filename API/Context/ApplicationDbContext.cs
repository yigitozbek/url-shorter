using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Context;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("LocalDb");
    }

    public DbSet<Analytic> Analytic { get; set; }
    public DbSet<Url> Url { get; set; }
}