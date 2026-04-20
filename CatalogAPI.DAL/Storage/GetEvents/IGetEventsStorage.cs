using CatalogAPI.Core.Enums;
using CatalogAPI.Core.Models;
using Homework.Ticketing.System.Shared.Models;

namespace CatalogAPI.DAL.Storage.GetEvents
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
