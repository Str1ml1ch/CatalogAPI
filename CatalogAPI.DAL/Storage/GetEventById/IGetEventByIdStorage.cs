using CatalogAPI.Core.Models;

namespace CatalogAPI.DAL.Storage.GetEventById
{
    public interface IGetEventByIdStorage
    {
        Task<bool> IsEventByIdExist(Guid id, CancellationToken ct);
        Task<EventModel?> GetEventByIdAsync(Guid id, CancellationToken ct);
    }
}
