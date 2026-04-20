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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.ManifestId).IsRequired();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).IsRequired(false).HasMaxLength(500);
                entity.Property(e => e.StartDate).IsRequired();
                entity.Property(e => e.EndDate).IsRequired();
                entity.Property(e => e.Status).IsRequired().HasConversion<string>().HasMaxLength(50);

                entity.HasIndex(e => e.ManifestId);
                entity.HasIndex(e => e.StartDate);
                entity.HasIndex(e => e.Status);

                entity.HasOne(e => e.SeatManifest)
                .WithMany(sm => sm.Events)
                .HasForeignKey(e => e.ManifestId);
            });

            modelBuilder.Entity<SeatManifest>(entity =>
            {
                entity.HasKey(sm => sm.Id);

                entity.Property(sm => sm.VenueId).IsRequired();
                entity.Property(sm => sm.Name).IsRequired().HasMaxLength(200);
                entity.Property(sm => sm.Capacity).IsRequired();

                entity.HasIndex(sm => sm.VenueId);

                entity.HasOne(sm => sm.Venue)
                .WithMany(v => v.SeatManifests)
                .HasForeignKey(sm => sm.VenueId);
            });

            modelBuilder.Entity<Seat>(entity =>
            {
                entity.HasKey(s => s.Id);

                entity.Property(s => s.SectionId).IsRequired();
                entity.Property(s => s.Row).IsRequired(false).HasMaxLength(20);
                entity.Property(s => s.Number).IsRequired(false).HasMaxLength(20);

                entity.HasIndex(s => new { s.SectionId, s.Row, s.Number });

                entity.HasOne(s => s.Section)
                .WithMany(sm => sm.Seats)
                .HasForeignKey(s => s.SectionId);
            });

            modelBuilder.Entity<Venue>(entity =>
            {
                entity.HasKey(v => v.Id);

                entity.Property(v => v.Name).IsRequired().HasMaxLength(200);
                entity.Property(v => v.Description).IsRequired(false).HasMaxLength(500);
                entity.Property(v => v.Address).IsRequired(false).HasMaxLength(200);
                entity.Property(v => v.City).IsRequired(false).HasMaxLength(100);
                entity.Property(v => v.Country).IsRequired(false).HasMaxLength(100);
                entity.Property(v => v.PostalCode).IsRequired(false).HasMaxLength(20);

                entity.HasIndex(v => v.City);
                entity.HasIndex(v => v.Country);

                entity.HasMany(v => v.SeatManifests)
                .WithOne(sm => sm.Venue)
                .HasForeignKey(sm => sm.VenueId);
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.HasKey(s => s.Id);

                entity.Property(s => s.ManifestId).IsRequired();
                entity.Property(s => s.Name).IsRequired().HasMaxLength(200);
                entity.Property(s => s.SeatType).IsRequired().HasConversion<string>().HasMaxLength(50);
                entity.Property(s => s.IsNumberedSeats).IsRequired();
                entity.Property(s => s.Capacity).IsRequired();
                entity.Property(s => s.Price).IsRequired().HasPrecision(18, 2);
                entity.Property(s => s.Currency).IsRequired().HasConversion<string>().HasMaxLength(10);
                entity.Property(s => s.Rows).IsRequired(false);
                entity.Property(s => s.SeatsPerRow).IsRequired(false);

                entity.HasIndex(s => new { s.ManifestId, s.SeatType });

                entity.HasOne(s => s.SeatManifest)
                .WithMany(sm => sm.Sections)
                .HasForeignKey(s => s.ManifestId);
            });
        }

        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {
        }
    }
}
