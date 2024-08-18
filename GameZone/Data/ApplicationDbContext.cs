using GameZone.Models;

namespace GameZone.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) { }

        
        public DbSet<Game> Games { get; set; }

        public DbSet<Categore> Categores { get; set; }

        public DbSet<Device> Devices { get; set; }

        public DbSet<GameDevice> GameDevices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GameDevice>().HasKey(e => new
            {
                e.GameId,
                e.DeviceId
            });

        }
    }
}
