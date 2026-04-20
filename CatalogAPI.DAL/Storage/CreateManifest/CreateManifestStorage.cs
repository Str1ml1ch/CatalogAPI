using CatalogAPI.DAL.Entities;

namespace CatalogAPI.DAL.Storage.CreateManifest
{
    public class CreateManifestStorage : ICreateManifestStorage
    {
        private readonly CatalogDbContext _context;

        public CreateManifestStorage(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Guid venueId, string name, int capacity, CancellationToken ct)
        {
            var manifest = new SeatManifest
            {
                Id = Guid.NewGuid(),
                VenueId = venueId,
                Name = name,
                Capacity = capacity,
                CreatedAt = DateTimeOffset.UtcNow
            };

            _context.SeatManifests.Add(manifest);
            await _context.SaveChangesAsync(ct);
        }
    }
}