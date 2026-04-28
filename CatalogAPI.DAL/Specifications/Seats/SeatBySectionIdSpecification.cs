using System.Linq.Expressions;
using CatalogAPI.DAL.Entities;

namespace CatalogAPI.DAL.Specifications.Seats
{
    public sealed class SeatBySectionIdSpecification : ISpecification<Seat>
    {
        private readonly Guid _sectionId;
        public SeatBySectionIdSpecification(Guid sectionId) => _sectionId = sectionId;
        public Expression<Func<Seat, bool>> ToExpression() => s => s.SectionId == _sectionId;
    }
}
