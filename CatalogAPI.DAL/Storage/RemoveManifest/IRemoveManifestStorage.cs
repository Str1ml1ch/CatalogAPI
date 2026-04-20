namespace CatalogAPI.DAL.Storage.RemoveManifest
{
    public interface IRemoveManifestStorage
    {
        Task RemoveManifestByIdAsync(Guid id, CancellationToken ct);
    }
}
