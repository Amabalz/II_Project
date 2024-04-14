using Project_II.Server.Models;
using Microsoft.EntityFrameworkCore;

    namespace Project_II.Server.Data
    {
        public class MyDbContext : DbContext
        {
            public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
            {
            }

            public DbSet<User> UserData { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<User>().HasKey(u => u.id);
            }
        }
    }