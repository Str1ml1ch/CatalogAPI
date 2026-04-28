using System.Linq.Expressions;
using CatalogAPI.DAL.Entities;

namespace CatalogAPI.DAL.Specifications.Events
{
    public sealed class EventByNameSpecification : ISpecification<Event>
    {
        private readonly string _name;
        public EventByNameSpecification(string name) => _name = name;
        public Expression<Func<Event, bool>> ToExpression() => e => e.Name.Contains(_name);
    }
}
