using CatalogAPI.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.DAL.Storage.GetManifestById
{
    public class GetManifestByIdStorage : IGetManifestByIdStorage
    {
        private readonly CatalogDbContext _context;

        public GetManifestByIdStorage(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task<SeatManifestDetailModel?> GetManifestByIdAsync(Guid id, CancellationToken ct)
        {
            return await _context.SeatManifests
                .Where(sm => sm.Id == id)
                .Select(sm => new SeatManifestDetailModel
                {
                    Id = sm.Id,
                    VenueId = sm.VenueId,
                    Name = sm.Name,
                    Capacity = sm.Capacity,
                    Seats = sm.Sections
                        .SelectMany(s => s.Seats)
                        .Select(s => new SeatModel
                        {
                            Id = s.Id,
                            SectionId = s.SectionId,
                            Row = s.Row,
                            Number = s.Number,
                            CreatedAt = s.CreatedAt,
                            UpdatedAt = s.UpdatedAt
                        }).ToList(),
                    CreatedAt = sm.CreatedAt,
                    UpdatedAt = sm.UpdatedAt
                })
                .FirstOrDefaultAsync(ct);
        }

        public async Task<bool> IsManifestByIdExist(Guid id, CancellationToken ct)
        {
            return await _context.SeatManifests.AnyAsync(sm => sm.Id == id, ct);
        }
    }
}
