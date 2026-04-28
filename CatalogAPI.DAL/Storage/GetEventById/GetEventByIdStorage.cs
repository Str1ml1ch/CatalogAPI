using CatalogAPI.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.DAL.Storage.GetEventById
{
    public class GetEventByIdStorage : IGetEventByIdStorage
    {
        private readonly CatalogDbContext _context;

        public GetEventByIdStorage(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task<EventModel?> GetEventByIdAsync(Guid id, CancellationToken ct)
        {
            return await _context.Events
                .Where(e => e.Id == id)
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
                .FirstOrDefaultAsync(ct);
        }

        public async Task<bool> IsEventByIdExistAsync(Guid id, CancellationToken ct)
        {
            return await _context.Events.AnyAsync(e => e.Id == id, ct);
        }
    }
}
