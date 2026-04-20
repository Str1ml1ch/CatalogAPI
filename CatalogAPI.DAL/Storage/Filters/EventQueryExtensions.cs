using CatalogAPI.DAL.Entities;
using CatalogAPI.Core.Enums;

namespace CatalogAPI.DAL.Storage.Filters
{
    public static class EventQueryExtensions
    {
        public static IQueryable<Event> FilterByManifestId(this IQueryable<Event> query, Guid? manifestId)
        {
            if (manifestId.HasValue)
                query = query.Where(e => e.ManifestId == manifestId.Value);
            return query;
        }

        public static IQueryable<Event> FilterByName(this IQueryable<Event> query, string? searchName)
        {
            if (!string.IsNullOrWhiteSpace(searchName))
                query = query.Where(e => e.Name.Contains(searchName));
            return query;
        }

        public static IQueryable<Event> FilterByStatus(this IQueryable<Event> query, EEventStatus? status)
        {
            if (status.HasValue)
                query = query.Where(e => e.Status == status.Value);
            return query;
        }

        public static IQueryable<Event> FilterByDateRange(this IQueryable<Event> query, DateTimeOffset? fromDate, DateTimeOffset? toDate)
        {
            if (fromDate.HasValue)
                query = query.Where(e => e.StartDate >= fromDate.Value);
            if (toDate.HasValue)
                query = query.Where(e => e.EndDate <= toDate.Value);
            return query;
        }
    }
}
