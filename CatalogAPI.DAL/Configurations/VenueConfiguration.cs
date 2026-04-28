using CatalogAPI.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogAPI.DAL.Configurations
{
    public class VenueConfiguration : IEntityTypeConfiguration<Venue>
    {
        public void Configure(EntityTypeBuilder<Venue> entity)
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
        }
    }
}
