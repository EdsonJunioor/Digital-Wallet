using DigitalWallet.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalWallet.DbContextConfig
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
            .Property(u => u.Bank)
            .HasConversion<int>();
        }
    }
}
