using CatalogAPI.Core.Enums;
using Homework.Ticketing.System.Shared.Enums;
using Shared.DAL.Entities;

namespace CatalogAPI.DAL.Entities
{
    public class Section : BaseDbEntity
    {
        public Guid ManifestId { get; set; }
        public string Name { get; set; } = null!;
        public ESeatType SeatType { get; set; }
        public bool IsNumberedSeats { get; set; }
        public int Capacity { get; set; }
        public decimal Price { get; set; }
        public ECurrency Currency { get; set; }
        public int? Rows { get; set; }
        public int? SeatsPerRow { get; set; }

        public virtual SeatManifest SeatManifest { get; set; } = null!;
        public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();
    }
}
