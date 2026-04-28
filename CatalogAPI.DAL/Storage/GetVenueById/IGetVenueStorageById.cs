using CatalogAPI.Core.Models;

namespace CatalogAPI.DAL.Storage.GetVenue
{
    public interface IGetVenueStorageById
    {
        Task<bool> IsVenueByIdExistAsync(Guid id, CancellationToken ct);
        Task<VenueModel?> GetVenueByIdAsync(Guid id, CancellationToken ct);
    }
}
