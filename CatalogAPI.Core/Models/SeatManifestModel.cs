namespace CatalogAPI.Core.Models
{
    public class SeatManifestModel
    {
        public Guid Id { get; set; }
        public Guid VenueId { get; set; }
        public string VenueName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int Capacity { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
