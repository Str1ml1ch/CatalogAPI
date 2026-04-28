using CatalogAPI.Core.Models;
using Homework.Ticketing.System.Shared.Models;

namespace CatalogAPI.DAL.Storage.GetSections
{
    public interface IGetSectionsStorage
    {
        Task<ResultModel<List<SectionModel>>> GetByVenueAsync(
            Guid venueId,
            Guid? manifestId,
            int page,
            int pageSize,
            CancellationToken ct);
    }
}
