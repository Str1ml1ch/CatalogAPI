using System.Linq.Expressions;
using CatalogAPI.Core.Enums;
using CatalogAPI.DAL.Entities;

namespace CatalogAPI.DAL.Specifications.Events
{
    public sealed class EventByStatusSpecification : ISpecification<Event>
    {
        private readonly EEventStatus _status;
        public EventByStatusSpecification(EEventStatus status) => _status = status;
        public Expression<Func<Event, bool>> ToExpression() => e => e.Status == _status;
    }
}
