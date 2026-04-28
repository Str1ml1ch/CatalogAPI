using CatalogAPI.Core.Models;

namespace CatalogAPI.DAL.Storage.GetManifestById
{
    public interface IGetManifestByIdStorage
    {
        Task<bool> IsManifestByIdExistAsync(Guid id, CancellationToken ct);
        Task<SeatManifestDetailModel?> GetManifestByIdAsync(Guid id, CancellationToken ct);
    }
}
