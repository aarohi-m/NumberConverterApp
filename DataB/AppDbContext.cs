using Microsoft.EntityFrameworkCore;
using NumberConverterApp.Models;

namespace NumberConverterApp.DataB
{

    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<ConversionHistory> ConversionHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.Entity<ConversionHistory>().HasKey(c => c.Id);
        }
    }
}