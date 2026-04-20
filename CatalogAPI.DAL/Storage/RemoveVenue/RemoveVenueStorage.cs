
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.DAL.Storage.RemoveVenue
{
    public class RemoveVenueStorage : IRemoveVenueStorage
    {
            private readonly CatalogDbContext _context;
        
            public RemoveVenueStorage(CatalogDbContext context)
            {
                _context = context;
            }

        public async Task RemoveVenueByIdAsync(Guid id, CancellationToken ct)
        {
            var venue = await _context.Venues.FirstOrDefaultAsync(v => v.Id == id, ct);

            _context.Venues.Remove(venue!);
            await _context.SaveChangesAsync(ct);
        }
    }
}
