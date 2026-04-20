using CatalogAPI.DAL.Entities;

namespace CatalogAPI.DAL.Storage.Filters
{
    public static class VenueQueryExtensions
    {
        public static IQueryable<Venue> FilterByName(this IQueryable<Venue> query, string? searchName)
        {
            if (!string.IsNullOrWhiteSpace(searchName))
                query = query.Where(v => v.Name.Contains(searchName));
            return query;
        }

        public static IQueryable<Venue> FilterByCity(this IQueryable<Venue> query, string? city)
        {
            if (!string.IsNullOrWhiteSpace(city))
                query = query.Where(v => v.City == city);
            return query;
        }

        public static IQueryable<Venue> FilterByCountry(this IQueryable<Venue> query, string? country)
        {
            if (!string.IsNullOrWhiteSpace(country))
                query = query.Where(v => v.Country == country);
            return query;
        }
    }
}
