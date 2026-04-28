using CatalogAPI.Core.Models;
using CatalogAPI.DAL.Specifications.Venues;
using Homework.Ticketing.System.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.DAL.Storage.GetVenues
{
    public class GetVenuesStorage : IGetVenuesStorage
    {
        private readonly CatalogDbContext _context;

        public GetVenuesStorage(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task<ResultModel<List<VenueModel>>> GetAsync(
            int page,
            int pageSize,
            string? searchName,
            string? city,
            string? country,
            CancellationToken ct)
        {
            var query = _context.Venues.AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchName))
                query = query.Where(new VenueByNameSpecification(searchName).ToExpression());
            if (!string.IsNullOrWhiteSpace(city))
                query = query.Where(new VenueByCitySpecification(city).ToExpression());
            if (!string.IsNullOrWhiteSpace(country))
                query = query.Where(new VenueByCountrySpecification(country).ToExpression());

            var totalCount = await query.CountAsync(ct);

            var items = await query
                .OrderBy(v => v.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
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
                .ToListAsync(ct);

            return new ResultModel<List<VenueModel>> { Data = items, Count = totalCount };
        }
    }
}
