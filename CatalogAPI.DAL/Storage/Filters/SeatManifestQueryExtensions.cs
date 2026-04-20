using CatalogAPI.DAL.Entities;

namespace CatalogAPI.DAL.Storage.Filters
{
    public static class SeatManifestQueryExtensions
    {
        public static IQueryable<SeatManifest> FilterByVenueId(this IQueryable<SeatManifest> query, Guid? venueId)
        {
            if (venueId.HasValue)
                query = query.Where(sm => sm.VenueId == venueId.Value);
            return query;
        }

        public static IQueryable<SeatManifest> FilterByName(this IQueryable<SeatManifest> query, string? searchName)
        {
            if (!string.IsNullOrWhiteSpace(searchName))
                query = query.Where(sm => sm.Name.Contains(searchName));
            return query;
        }
    }
}
