using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.DAL.Storage.GetSectionById
{
    public class GetSectionByIdStorage : IGetSectionByIdStorage
    {
        private readonly CatalogDbContext _context;

        public GetSectionByIdStorage(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsSectionByIdExistAsync(Guid id, CancellationToken ct)
        {
            return await _context.Sections.AnyAsync(s => s.Id == id, ct);
        }
    }
}
