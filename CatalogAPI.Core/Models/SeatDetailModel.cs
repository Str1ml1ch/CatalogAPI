using CatalogAPI.Core.Enums;

namespace CatalogAPI.Core.Models
{
    public class SeatDetailModel
    {
        public Guid Id { get; set; }
        public Guid SectionId { get; set; }
        public string? Row { get; set; }
        public string? Number { get; set; }
        public ESeatStatus Status { get; set; } = ESeatStatus.Available;
        public SeatPriceOptionModel PriceOption { get; set; } = null!;
    }
}
