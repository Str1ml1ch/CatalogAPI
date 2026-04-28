using System.Linq.Expressions;
using CatalogAPI.DAL.Entities;

namespace CatalogAPI.DAL.Specifications.SeatManifests
{
    public sealed class SeatManifestByVenueIdSpecification : ISpecification<SeatManifest>
    {
        private readonly Guid _venueId;
        public SeatManifestByVenueIdSpecification(Guid venueId) => _venueId = venueId;
        public Expression<Func<SeatManifest, bool>> ToExpression() => sm => sm.VenueId == _venueId;
    }
}
