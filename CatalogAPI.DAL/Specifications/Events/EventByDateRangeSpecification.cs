using System.Linq.Expressions;
using CatalogAPI.DAL.Entities;

namespace CatalogAPI.DAL.Specifications.Events
{
    public sealed class EventByDateRangeSpecification : ISpecification<Event>
    {
        private readonly DateTimeOffset? _fromDate;
        private readonly DateTimeOffset? _toDate;

        public EventByDateRangeSpecification(DateTimeOffset? fromDate, DateTimeOffset? toDate)
        {
            _fromDate = fromDate;
            _toDate = toDate;
        }

        public Expression<Func<Event, bool>> ToExpression()
            => e => (!_fromDate.HasValue || e.StartDate >= _fromDate.Value)
                 && (!_toDate.HasValue || e.EndDate <= _toDate.Value);
    }
}
