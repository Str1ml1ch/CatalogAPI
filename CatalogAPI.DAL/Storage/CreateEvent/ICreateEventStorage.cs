using CatalogAPI.Core.Enums;

namespace CatalogAPI.DAL.Storage.CreateEvent
{
    public interface ICreateEventStorage
    {
        Task CreateAsync(Guid manifestId, string name, string? description, DateTimeOffset startDate, DateTimeOffset endDate, EEventStatus status, CancellationToken ct);
    }
}
