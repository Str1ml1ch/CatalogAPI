using CatalogAPI.Domain.Models;

namespace CatalogAPI.Domain.Storage.GetEventById
{
    public interface IGetEventByIdStorage
    {
        Task<bool> IsEventByIdExistAsync(Guid id, CancellationToken ct);
        Task<EventModel?> GetEventByIdAsync(Guid id, CancellationToken ct);
    }
}
