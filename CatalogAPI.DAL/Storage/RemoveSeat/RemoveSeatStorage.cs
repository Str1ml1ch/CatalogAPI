using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.DAL.Storage.RemoveSeat
{
    public class RemoveSeatStorage : IRemoveSeatStorage
    {
        private readonly CatalogDbContext _context;

        public RemoveSeatStorage(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task RemoveSeatByIdAsync(Guid id, CancellationToken ct)
        {
            var seat = await _context.Seats.FirstOrDefaultAsync(s => s.Id == id, ct);

            _context.Seats.Remove(seat!);
            await _context.SaveChangesAsync(ct);
        }

        public async Task RemoveAllByManifestIdAsync(Guid manifestId, CancellationToken ct)
        {
            var seats = await _context.Seats
                .Where(s => s.Section.ManifestId == manifestId)
                .ToListAsync(ct);

            _context.Seats.RemoveRange(seats);
            await _context.SaveChangesAsync(ct);
        }
    }
}
