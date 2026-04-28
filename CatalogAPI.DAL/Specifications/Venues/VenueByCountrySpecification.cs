using System.Linq.Expressions;
using CatalogAPI.DAL.Entities;

namespace CatalogAPI.DAL.Specifications.Venues
{
    public sealed class VenueByCountrySpecification : ISpecification<Venue>
    {
        private readonly string _country;
        public VenueByCountrySpecification(string country) => _country = country;
        public Expression<Func<Venue, bool>> ToExpression() => v => v.Country == _country;
    }
}
