using ChurrasTrica.Domain.Entities;
using ChurrasTrinca.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChurrasTrica.Data.Data
{
    public class DataContext : DbContext
    {
        public DbSet<ChurrasEntity> Churras { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<LoginEntity> Login { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Maps.ChurrasMap());
            modelBuilder.ApplyConfiguration(new Maps.UserMap());

            modelBuilder.Entity<LoginEntity>().HasData(
                new LoginEntity
                {
                    Id = 1,
                    Name = "Thomaz",
                    Email = "queroChurras@trinca.com",
                    Password = "trinca123"
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
