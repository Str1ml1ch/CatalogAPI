using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.DAL.Storage.UpdateManifest
{
    public class UpdateManifestStorage : IUpdateManifestStorage
    {
        private readonly CatalogDbContext _context;

        public UpdateManifestStorage(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task UpdateAsync(Guid id, string name, int capacity, CancellationToken ct)
        {
            var manifest = await _context.SeatManifests.FirstOrDefaultAsync(sm => sm.Id == id, ct);
            manifest!.Name = name;
            manifest.Capacity = capacity;
            manifest.UpdatedAt = DateTimeOffset.UtcNow;

            await _context.SaveChangesAsync(ct);
        }
    }
}