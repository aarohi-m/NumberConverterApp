using Microsoft.EntityFrameworkCore;
using NumberConverterApp.Models;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<ConversionHistory> ConversionHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ConversionHistory>().HasKey(c => c.Id);
    }
}