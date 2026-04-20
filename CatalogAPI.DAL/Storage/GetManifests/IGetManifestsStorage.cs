using CatalogAPI.Core.Models;
using Homework.Ticketing.System.Shared.Models;

namespace CatalogAPI.DAL.Storage.GetManifests
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
