using CatalogAPI.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogAPI.DAL.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> entity)
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
        }
    }
}
