namespace CatalogAPI.Core.Models
{
    public class SeatModel
    {
        public Guid Id { get; set; }
        public Guid SectionId { get; set; }
        public string? Row { get; set; }
        public string? Number { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
