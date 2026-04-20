using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.DAL.Storage.RemoveManifest
{
    public class RemoveManifestStorage : IRemoveManifestStorage
    {
        private readonly CatalogDbContext _context;

        public RemoveManifestStorage(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task RemoveManifestByIdAsync(Guid id, CancellationToken ct)
        {
            var manifest = await _context.SeatManifests.FirstOrDefaultAsync(sm => sm.Id == id, ct);

            _context.SeatManifests.Remove(manifest!);
            await _context.SaveChangesAsync(ct);
        }
    }
}
