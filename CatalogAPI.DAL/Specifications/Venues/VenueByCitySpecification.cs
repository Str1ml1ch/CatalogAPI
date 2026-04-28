using System.Linq.Expressions;
using CatalogAPI.DAL.Entities;

namespace CatalogAPI.DAL.Specifications.Venues
{
    public sealed class VenueByCitySpecification : ISpecification<Venue>
    {
        private readonly string _city;
        public VenueByCitySpecification(string city) => _city = city;
        public Expression<Func<Venue, bool>> ToExpression() => v => v.City == _city;
    }
}
