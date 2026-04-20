namespace CatalogAPI.DAL.Storage.RemoveVenue
{
    public interface IRemoveVenueStorage
    {
        Task RemoveVenueByIdAsync(Guid id, CancellationToken ct);
    }
}
