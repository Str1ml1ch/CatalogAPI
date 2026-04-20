using CatalogAPI.DAL.Entities;

namespace CatalogAPI.DAL.Storage
{
    public class CreateVenueStorage : ICrateVenueStorage
    {
        private readonly CatalogDbContext _context;
        
        public CreateVenueStorage(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(string name, string? description, string? address, string? city, string? country, string? postalCode, CancellationToken ct)
        {
            var venue = new Venue
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description,
                Address = address,
                City = city,
                Country = country,
                PostalCode = postalCode
            };

            _context.Venues.Add(venue);
             await _context.SaveChangesAsync(ct);
        }
    }
}
