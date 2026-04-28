using System.Linq.Expressions;
using CatalogAPI.DAL.Entities;

namespace CatalogAPI.DAL.Specifications.Seats
{
    public sealed class SeatByManifestIdSpecification : ISpecification<Seat>
    {
        private readonly Guid _manifestId;
        public SeatByManifestIdSpecification(Guid manifestId) => _manifestId = manifestId;
        public Expression<Func<Seat, bool>> ToExpression() => s => s.Section.ManifestId == _manifestId;
    }
}
