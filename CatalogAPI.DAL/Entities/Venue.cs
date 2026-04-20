using Shared.DAL.Entities;

namespace CatalogAPI.DAL.Entities
{
    public class Venue : BaseDbEntity
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }

        public virtual ICollection<SeatManifest> SeatManifests { get; set; } = new List<SeatManifest>();
    }
}
