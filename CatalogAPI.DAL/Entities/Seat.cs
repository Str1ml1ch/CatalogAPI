using Shared.DAL.Entities;

namespace CatalogAPI.DAL.Entities
{
    public class Seat : BaseDbEntity
    {
        public Guid SectionId { get; set; }
        public string? Row { get; set; }
        public string? Number { get; set; }

        public virtual Section Section { get; set; } = null!;
    }
}
