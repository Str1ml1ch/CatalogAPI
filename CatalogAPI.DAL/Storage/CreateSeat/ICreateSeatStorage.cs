namespace CatalogAPI.DAL.Storage.CreateSeat
{
    public interface ICreateSeatStorage
    {
        Task CreateAsync(Guid sectionId, string? row, string? number, CancellationToken ct);
        Task CreateBatchAsync(Guid sectionId, IEnumerable<(string? Row, string? Number)> seats, CancellationToken ct);
    }
}
