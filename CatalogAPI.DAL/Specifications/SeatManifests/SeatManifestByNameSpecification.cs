using System.Linq.Expressions;
using CatalogAPI.DAL.Entities;

namespace CatalogAPI.DAL.Specifications.SeatManifests
{
    public sealed class SeatManifestByNameSpecification : ISpecification<SeatManifest>
    {
        private readonly string _name;
        public SeatManifestByNameSpecification(string name) => _name = name;
        public Expression<Func<SeatManifest, bool>> ToExpression() => sm => sm.Name.Contains(_name);
    }
}
