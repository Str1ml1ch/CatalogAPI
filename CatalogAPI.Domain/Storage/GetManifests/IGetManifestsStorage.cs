using CatalogAPI.Domain.Models;
using Homework.Ticketing.System.Shared.Models;

namespace CatalogAPI.Domain.Storage.GetManifests
{
    public interface IGetManifestsStorage
    {
        Task<ResultModel<List<SeatManifestModel>>> GetAsync(
            int page,
            int pageSize,
            Guid? venueId,
            string? searchName,
            CancellationToken ct);
    }
}
