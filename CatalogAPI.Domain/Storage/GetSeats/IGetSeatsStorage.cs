using CatalogAPI.Domain.Models;
using Homework.Ticketing.System.Shared.Models;

namespace CatalogAPI.Domain.Storage.GetSeats
{
    public interface IGetSeatsStorage
    {
        Task<ResultModel<List<SeatModel>>> GetAsync(
            int page,
            int pageSize,
            Guid manifestId,
            Guid? sectionId,
            CancellationToken ct);
    }
}
