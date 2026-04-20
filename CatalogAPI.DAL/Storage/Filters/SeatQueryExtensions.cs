using CatalogAPI.DAL.Entities;

namespace CatalogAPI.DAL.Storage.Filters
{
    public static class SeatQueryExtensions
    {
        public static IQueryable<Seat> FilterByManifestId(this IQueryable<Seat> query, Guid manifestId)
        {
            return query.Where(s => s.Section.ManifestId == manifestId);
        }

        public static IQueryable<Seat> FilterBySectionId(this IQueryable<Seat> query, Guid? sectionId)
        {
            if (sectionId.HasValue)
                query = query.Where(s => s.SectionId == sectionId.Value);
            return query;
        }
    }
}