using CatalogAPI.Core.Enums;
using Shared.DAL.Entities;

namespace CatalogAPI.DAL.Entities
{
    public class Event : BaseDbEntity
    {
        public Guid ManifestId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public EEventStatus Status { get; set; }

        public virtual SeatManifest SeatManifest { get; set; } = null!;

    }
}
