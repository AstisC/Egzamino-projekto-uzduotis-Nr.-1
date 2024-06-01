using Microsoft.EntityFrameworkCore;
using PasswordManagerAPI.Models;

namespace PasswordManagerAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<PasswordEntry> PasswordEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<PasswordEntry>()
                .HasOne(p => p.User)
                .WithMany(u => u.PasswordEntries)
                .HasForeignKey(p => p.UserId);
        }
    }
}
