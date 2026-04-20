using CatalogAPI.Core.Models;
using Homework.Ticketing.System.Shared.Models;

namespace CatalogAPI.DAL.Storage.GetSeats
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
