using CatalogAPI.Domain.Enums;

namespace CatalogAPI.Domain.Models
{
    public class EventModel
    {
        public Guid Id { get; set; }
        public Guid ManifestId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public EEventStatus Status { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
