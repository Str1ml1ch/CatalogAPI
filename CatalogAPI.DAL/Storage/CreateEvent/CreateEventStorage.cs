using CatalogAPI.DAL.Entities;
using CatalogAPI.Core.Enums;

namespace CatalogAPI.DAL.Storage.CreateEvent
{
    public class CreateEventStorage : ICreateEventStorage
    {
        private readonly CatalogDbContext _context;

        public CreateEventStorage(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Guid manifestId, string name, string? description, DateTimeOffset startDate, DateTimeOffset endDate, EEventStatus status, CancellationToken ct)
        {
            var @event = new Event
            {
                Id = Guid.NewGuid(),
                ManifestId = manifestId,
                Name = name,
                Description = description,
                StartDate = startDate,
                EndDate = endDate,
                Status = status,
                CreatedAt = DateTimeOffset.UtcNow
            };

            _context.Events.Add(@event);
            await _context.SaveChangesAsync(ct);
        }
    }
}
