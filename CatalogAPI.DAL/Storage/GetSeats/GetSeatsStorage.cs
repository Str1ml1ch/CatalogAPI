using CatalogAPI.Core.Models;
using CatalogAPI.DAL.Storage.Filters;
using Homework.Ticketing.System.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.DAL.Storage.GetSeats
{
    public class GetSeatsStorage : IGetSeatsStorage
    {
        private readonly CatalogDbContext _context;

        public GetSeatsStorage(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task<ResultModel<List<SeatModel>>> GetAsync(
            int page,
            int pageSize,
            Guid manifestId,
            Guid? sectionId,
            CancellationToken ct)
        {
            var query = _context.Seats
                .FilterByManifestId(manifestId)
                .FilterBySectionId(sectionId);

            var totalCount = await query.CountAsync(ct);

            var items = await query
                .OrderBy(s => s.Row)
                .ThenBy(s => s.Number)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new SeatModel
                {
                    Id = s.Id,
                    SectionId = s.SectionId,
                    Row = s.Row,
                    Number = s.Number,
                    CreatedAt = s.CreatedAt,
                    UpdatedAt = s.UpdatedAt
                })
                .ToListAsync(ct);

            return new ResultModel<List<SeatModel>> { Data = items, Count = totalCount };
        }
    }
}
