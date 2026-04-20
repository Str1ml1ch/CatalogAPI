using CatalogAPI.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.DAL.Storage.GetVenue
{
    public class GetVenueStorageById : IGetVenueStorageById
    {
        private readonly CatalogDbContext _context;

        public GetVenueStorageById(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task<VenueModel?> GetVenueByIdAsync(Guid id, CancellationToken ct)
        {
            return await _context.Venues
                .Where(v => v.Id == id)
                .Select(v => new VenueModel
                {
                    Id = v.Id,
                    Name = v.Name,
                    Description = v.Description,
                    Address = v.Address,
                    City = v.City,
                    Country = v.Country,
                    PostalCode = v.PostalCode,
                    CreatedAt = v.CreatedAt,
                    UpdatedAt = v.UpdatedAt
                })
                .FirstOrDefaultAsync(ct);
        }

        public async Task<bool> IsVenueByIdExist(Guid id, CancellationToken ct)
        {
            return await _context.Venues.AnyAsync(v => v.Id == id, ct);
        }
    }
}
