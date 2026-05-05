using CatalogAPI.Domain.Models;
using Homework.Ticketing.System.Shared.Models;

namespace CatalogAPI.Domain.Storage.GetVenues
{
    public interface IGetVenuesStorage
    {
        Task<ResultModel<List<VenueModel>>> GetAsync(
            int page,
            int pageSize,
            string? searchName,
            string? city,
            string? country,
            CancellationToken ct);
    }
}
