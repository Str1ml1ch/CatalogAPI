using CatalogAPI.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.DAL
{
    public sealed class CatalogDbContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<SeatManifest> SeatManifests { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Section> Sections { get; set; }

        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);
        }
    }
}
