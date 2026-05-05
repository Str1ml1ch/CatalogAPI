using CatalogAPI.Domain.Enums;
using CatalogAPI.Domain.Models;
using Homework.Ticketing.System.Shared.Models;

namespace CatalogAPI.Domain.Storage.GetEvents
{
    public interface IGetEventsStorage
    {
        Task<ResultModel<List<EventModel>>> GetAsync(
            int page,
            int pageSize,
            Guid? manifestId,
            string? searchName,
            EEventStatus? status,
            DateTimeOffset? fromDate,
            DateTimeOffset? toDate,
            CancellationToken ct);
    }
}
