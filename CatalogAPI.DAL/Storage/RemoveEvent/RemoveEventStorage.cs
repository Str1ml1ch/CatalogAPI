using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.DAL.Storage.RemoveEvent
{
    public class RemoveEventStorage : IRemoveEventStorage
    {
        private readonly CatalogDbContext _context;

        public RemoveEventStorage(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task RemoveEventByIdAsync(Guid id, CancellationToken ct)
        {
            var @event = await _context.Events.FirstOrDefaultAsync(e => e.Id == id, ct);

            _context.Events.Remove(@event!);
            await _context.SaveChangesAsync(ct);
        }
    }
}
