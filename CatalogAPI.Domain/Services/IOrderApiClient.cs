namespace CatalogAPI.Domain.Services
{
    public interface IOrderApiClient
    {
        Task<Dictionary<Guid, int>> GetSeatStatusesAsync(IEnumerable<Guid> seatIds, CancellationToken ct);
    }
}
