using CatalogAPI.Core.Models;
using Homework.Ticketing.System.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.DAL.Storage.GetEventSectionSeats
{
    public class GetEventSectionSeatsStorage : IGetEventSectionSeatsStorage
    {
        private readonly CatalogDbContext _context;

        public GetEventSectionSeatsStorage(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task<ResultModel<List<SeatDetailModel>>> GetAsync(
            Guid eventId,
            Guid sectionId,
            int page,
            int pageSize,
            CancellationToken ct)
        {
            var query = _context.Seats
                .Where(s => s.SectionId == sectionId
                    && s.Section.SeatManifest.Events.Any(e => e.Id == eventId));

            var totalCount = await query.CountAsync(ct);

            var items = await query
                .OrderBy(s => s.Row)
                .ThenBy(s => s.Number)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new SeatDetailModel
                {
                    Id = s.Id,
                    SectionId = s.SectionId,
                    Row = s.Row,
                    Number = s.Number,
                    PriceOption = new SeatPriceOptionModel
                    {
                        Id = (int)s.Section.SeatType,
                        Name = s.Section.SeatType.ToString(),
                        Price = s.Section.Price,
                        Currency = s.Section.Currency
                    }
                })
                .ToListAsync(ct);

            return new ResultModel<List<SeatDetailModel>> { Data = items, Count = totalCount };
        }
    }
}
