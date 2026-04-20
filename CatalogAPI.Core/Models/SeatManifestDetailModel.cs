namespace CatalogAPI.Core.Models
{
    public class SeatManifestDetailModel
    {
        public Guid Id { get; set; }
        public Guid VenueId { get; set; }
        public string Name { get; set; } = null!;
        public int Capacity { get; set; }
        public List<SeatModel> Seats { get; set; } = [];
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
