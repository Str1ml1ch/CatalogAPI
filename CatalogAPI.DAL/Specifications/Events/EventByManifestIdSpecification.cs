using System.Linq.Expressions;
using CatalogAPI.DAL.Entities;

namespace CatalogAPI.DAL.Specifications.Events
{
    public sealed class EventByManifestIdSpecification : ISpecification<Event>
    {
        private readonly Guid _manifestId;
        public EventByManifestIdSpecification(Guid manifestId) => _manifestId = manifestId;
        public Expression<Func<Event, bool>> ToExpression() => e => e.ManifestId == _manifestId;
    }
}
