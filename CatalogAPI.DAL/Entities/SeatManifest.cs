using Shared.DAL.Entities;

namespace CatalogAPI.DAL.Entities
{
    public class SeatManifest : BaseDbEntity
    {
        public Guid VenueId { get; set; }
        public string Name { get; set; } = null!;
        public int Capacity { get; set; }

        public virtual Venue Venue { get; set; } = null!;
        public virtual ICollection<Event> Events { get; set; } = new List<Event>();
        public virtual ICollection<Section> Sections { get; set; } = new List<Section>();

    }
}
