using CatalogAPI.Core.Enums;
using CatalogAPI.Core.Models;
using CatalogAPI.DAL.Specifications.Events;
using Homework.Ticketing.System.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.DAL.Storage.GetEvents
{
    public class GetEventsStorage : IGetEventsStorage
    {
        private readonly CatalogDbContext _context;

        public GetEventsStorage(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task<ResultModel<List<EventModel>>> GetAsync(
            int page,
            int pageSize,
            Guid? manifestId,
            string? searchName,
            EEventStatus? status,
            DateTimeOffset? fromDate,
            DateTimeOffset? toDate,
            CancellationToken ct)
        {
            var query = _context.Events.AsQueryable();
            if (manifestId.HasValue)
                query = query.Where(new EventByManifestIdSpecification(manifestId.Value).ToExpression());
            if (!string.IsNullOrWhiteSpace(searchName))
                query = query.Where(new EventByNameSpecification(searchName).ToExpression());
            if (status.HasValue)
                query = query.Where(new EventByStatusSpecification(status.Value).ToExpression());
            if (fromDate.HasValue || toDate.HasValue)
                query = query.Where(new EventByDateRangeSpecification(fromDate, toDate).ToExpression());

            var totalCount = await query.CountAsync(ct);

            var items = await query
                .OrderBy(e => e.StartDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(e => new EventModel
                {
                    Id = e.Id,
                    ManifestId = e.ManifestId,
                    Name = e.Name,
                    Description = e.Description,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    Status = e.Status,
                    CreatedAt = e.CreatedAt,
                    UpdatedAt = e.UpdatedAt
                })
                .ToListAsync(ct);

            return new ResultModel<List<EventModel>> { Count = totalCount, Data = items };
        }
    }
}
