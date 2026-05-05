using CatalogAPI.Domain.Models;

namespace CatalogAPI.Domain.Storage.GetManifestById
{
    public interface IGetManifestByIdStorage
    {
        Task<bool> IsManifestByIdExistAsync(Guid id, CancellationToken ct);
        Task<SeatManifestDetailModel?> GetManifestByIdAsync(Guid id, CancellationToken ct);
    }
}
