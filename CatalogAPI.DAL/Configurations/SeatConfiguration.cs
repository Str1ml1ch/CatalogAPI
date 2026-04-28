using CatalogAPI.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogAPI.DAL.Configurations
{
    public class SeatConfiguration : IEntityTypeConfiguration<Seat>
    {
        public void Configure(EntityTypeBuilder<Seat> entity)
        {
            entity.HasKey(s => s.Id);

            entity.Property(s => s.SectionId).IsRequired();
            entity.Property(s => s.Row).IsRequired(false).HasMaxLength(20);
            entity.Property(s => s.Number).IsRequired(false).HasMaxLength(20);

            entity.HasIndex(s => new { s.SectionId, s.Row, s.Number });

            entity.HasOne(s => s.Section)
                .WithMany(sm => sm.Seats)
                .HasForeignKey(s => s.SectionId);
        }
    }
}
