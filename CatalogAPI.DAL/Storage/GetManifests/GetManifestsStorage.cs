using CatalogAPI.Core.Models;
using CatalogAPI.DAL.Storage.Filters;
using Homework.Ticketing.System.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.DAL.Storage.GetManifests
{
    public class GetManifestsStorage : IGetManifestsStorage
    {
        private readonly CatalogDbContext _context;

        public GetManifestsStorage(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task<ResultModel<List<SeatManifestModel>>> GetAsync(
            int page,
            int pageSize,
            Guid? venueId,
            string? searchName,
            CancellationToken ct)
        {
            var query = _context.SeatManifests
                .FilterByVenueId(venueId)
                .FilterByName(searchName);

            var totalCount = await query.CountAsync(ct);

            var items = await query
                .OrderBy(sm => sm.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(sm => new SeatManifestModel
                {
                    Id = sm.Id,
                    VenueId = sm.VenueId,
                    VenueName = sm.Venue.Name,
                    Name = sm.Name,
                    Capacity = sm.Capacity,
                    CreatedAt = sm.CreatedAt,
                    UpdatedAt = sm.UpdatedAt
                })
                .ToListAsync(ct);

            return new ResultModel<List<SeatManifestModel>> { Data = items, Count = totalCount };
        }
    }
}
