using CatalogAPI.Core.Enums;
using Homework.Ticketing.System.Shared.Enums;

namespace CatalogAPI.Core.Models
{
    public class SectionModel
    {
        public Guid Id { get; set; }
        public Guid ManifestId { get; set; }
        public string Name { get; set; } = null!;
        public ESeatType SeatType { get; set; }
        public bool IsNumberedSeats { get; set; }
        public int Capacity { get; set; }
        public decimal Price { get; set; }
        public ECurrency Currency { get; set; }
        public int? Rows { get; set; }
        public int? SeatsPerRow { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
