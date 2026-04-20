namespace CatalogAPI.DAL.Storage.RemoveEvent
{
    public interface IRemoveEventStorage
    {
        Task RemoveEventByIdAsync(Guid id, CancellationToken ct);
    }
}
