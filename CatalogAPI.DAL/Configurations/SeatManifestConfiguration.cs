using CatalogAPI.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogAPI.DAL.Configurations
{
    public class SeatManifestConfiguration : IEntityTypeConfiguration<SeatManifest>
    {
        public void Configure(EntityTypeBuilder<SeatManifest> entity)
        {
            entity.HasKey(sm => sm.Id);

            entity.Property(sm => sm.VenueId).IsRequired();
            entity.Property(sm => sm.Name).IsRequired().HasMaxLength(200);
            entity.Property(sm => sm.Capacity).IsRequired();

            entity.HasIndex(sm => sm.VenueId);

            entity.HasOne(sm => sm.Venue)
                .WithMany(v => v.SeatManifests)
                .HasForeignKey(sm => sm.VenueId);
        }
    }
}
