using CatalogAPI.Domain.Models;
using Homework.Ticketing.System.Shared.Models;

namespace CatalogAPI.Domain.Storage.GetEventSectionSeats
{
    public interface IGetEventSectionSeatsStorage
    {
        Task<ResultModel<List<SeatDetailModel>>> GetAsync(
            Guid eventId,
            Guid sectionId,
            int page,
            int pageSize,
            CancellationToken ct);
    }
}
