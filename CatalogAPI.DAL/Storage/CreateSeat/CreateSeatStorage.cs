using CatalogAPI.DAL.Entities;

namespace CatalogAPI.DAL.Storage.CreateSeat
{
    public class CreateSeatStorage : ICreateSeatStorage
    {
        private readonly CatalogDbContext _context;

        public CreateSeatStorage(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Guid sectionId, string? row, string? number, CancellationToken ct)
        {
            var seat = new Seat
            {
                Id = Guid.NewGuid(),
                SectionId = sectionId,
                Row = row,
                Number = number,
                CreatedAt = DateTimeOffset.UtcNow
            };

            _context.Seats.Add(seat);
            await _context.SaveChangesAsync(ct);
        }

        public async Task CreateBatchAsync(Guid sectionId, IEnumerable<(string? Row, string? Number)> seats, CancellationToken ct)
        {
            var entities = seats.Select(s => new Seat
            {
                Id = Guid.NewGuid(),
                SectionId = sectionId,
                Row = s.Row,
                Number = s.Number,
                CreatedAt = DateTimeOffset.UtcNow
            });

            _context.Seats.AddRange(entities);
            await _context.SaveChangesAsync(ct);
        }
    }
}