using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<ConversionHistory> ConversionHistories { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ConversionHistory>().HasKey(c => c.Id);
    }
}