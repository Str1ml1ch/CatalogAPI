using CatalogAPI.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.DAL.Storage.UpdateEvent
{
    public class UpdateEventStorage : IUpdateEventStorage
    {
        private readonly CatalogDbContext _context;

        public UpdateEventStorage(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task UpdateAsync(Guid id, string name, string? description, DateTimeOffset startDate, DateTimeOffset endDate, EEventStatus status, CancellationToken ct)
        {
            var @event = await _context.Events.FirstOrDefaultAsync(e => e.Id == id, ct);

            @event.Name = name;
            @event.Description = description;
            @event.StartDate = startDate;
            @event.EndDate = endDate;
            @event.Status = status;
            @event.UpdatedAt = DateTimeOffset.UtcNow;

            await _context.SaveChangesAsync(ct);
        }
    }
}
