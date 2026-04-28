using System.Linq.Expressions;
using CatalogAPI.DAL.Entities;

namespace CatalogAPI.DAL.Specifications.Venues
{
    public sealed class VenueByNameSpecification : ISpecification<Venue>
    {
        private readonly string _name;
        public VenueByNameSpecification(string name) => _name = name;
        public Expression<Func<Venue, bool>> ToExpression() => v => v.Name.Contains(_name);
    }
}
