using CatalogAPI.Core.Models;
using Homework.Ticketing.System.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.DAL.Storage.GetSections
{
    public class GetSectionsStorage : IGetSectionsStorage
    {
        private readonly CatalogDbContext _context;

        public GetSectionsStorage(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task<ResultModel<List<SectionModel>>> GetByVenueAsync(
            Guid venueId,
            Guid? manifestId,
            int page,
            int pageSize,
            CancellationToken ct)
        {
            var query = _context.Sections
                .Where(s => s.SeatManifest.VenueId == venueId);

            if (manifestId.HasValue)
                query = query.Where(s => s.ManifestId == manifestId.Value);

            var totalCount = await query.CountAsync(ct);

            var items = await query
                .OrderBy(s => s.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new SectionModel
                {
                    Id = s.Id,
                    ManifestId = s.ManifestId,
                    Name = s.Name,
                    SeatType = s.SeatType,
                    IsNumberedSeats = s.IsNumberedSeats,
                    Capacity = s.Capacity,
                    Price = s.Price,
                    Currency = s.Currency,
                    Rows = s.Rows,
                    SeatsPerRow = s.SeatsPerRow,
                    CreatedAt = s.CreatedAt,
                    UpdatedAt = s.UpdatedAt
                })
                .ToListAsync(ct);

            return new ResultModel<List<SectionModel>> { Data = items, Count = totalCount };
        }
    }
}
