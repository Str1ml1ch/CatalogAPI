using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.DAL.Storage.UpdateVenue
{
    public class UpdateVenueStorage : IUpdateVenueStorage
    {
        private readonly CatalogDbContext _context;

        public UpdateVenueStorage(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task UpdateAsync(Guid id, string name, string? description, string? address, string? city, string? country, string? postalCode, CancellationToken ct)
        {
            var venue = await _context.Venues.FirstOrDefaultAsync(v => v.Id == id, ct);
            
            venue.Name = name;
            venue.Description = description;
            venue.Address = address;
            venue.City = city;
            venue.Country = country;
            venue.PostalCode = postalCode;
            venue.UpdatedAt = DateTimeOffset.UtcNow;

            await _context.SaveChangesAsync(ct);
        }
    }
}
