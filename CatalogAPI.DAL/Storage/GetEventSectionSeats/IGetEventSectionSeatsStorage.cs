using CatalogAPI.Core.Models;
using Homework.Ticketing.System.Shared.Models;

namespace CatalogAPI.DAL.Storage.GetEventSectionSeats
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
