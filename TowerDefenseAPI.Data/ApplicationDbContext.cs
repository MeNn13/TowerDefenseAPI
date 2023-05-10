using Microsoft.EntityFrameworkCore;
using TowerDefenseAPI.Domain.Models;

namespace TowerDefenseAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Login = "Menn",
                    Password = BCrypt.Net.BCrypt.HashPassword("1234"),
                    Role = "Player",
                    Score = 0,
                },
                new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Login = "Fille",
                    Password = BCrypt.Net.BCrypt.HashPassword("1234"),
                    Role = "Admin",
                    Score = 0,
                });
        }
    }
}
