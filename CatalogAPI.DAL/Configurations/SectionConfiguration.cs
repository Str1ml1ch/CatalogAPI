using CatalogAPI.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogAPI.DAL.Configurations
{
    public class SectionConfiguration : IEntityTypeConfiguration<Section>
    {
        public void Configure(EntityTypeBuilder<Section> entity)
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
        }
    }
}
