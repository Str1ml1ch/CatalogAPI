using System.Linq.Expressions;

namespace CatalogAPI.DAL.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> ToExpression();
    }
}
