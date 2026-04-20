using CatalogAPI.Core.Enums;

namespace CatalogAPI.DAL.Storage.UpdateEvent
{
    public interface IUpdateEventStorage
    {
        Task UpdateAsync(Guid id, string name, string? description, DateTimeOffset startDate, DateTimeOffset endDate, EEventStatus status, CancellationToken ct);
    }
}
