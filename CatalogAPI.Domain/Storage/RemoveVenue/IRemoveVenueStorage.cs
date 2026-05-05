namespace CatalogAPI.Domain.Storage.RemoveVenue
{
    public interface IRemoveVenueStorage
    {
        Task RemoveVenueByIdAsync(Guid id, CancellationToken ct);
    }
}
