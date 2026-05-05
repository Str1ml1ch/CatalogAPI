using CatalogAPI.Domain.Enums;

namespace CatalogAPI.Domain.Storage.UpdateEvent
{
    public interface IUpdateEventStorage
    {
        Task UpdateAsync(Guid id, string name, string? description, DateTimeOffset startDate, DateTimeOffset endDate, EEventStatus status, CancellationToken ct);
    }
}
