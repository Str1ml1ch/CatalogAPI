namespace CatalogAPI.Domain.Storage.RemoveManifest
{
    public interface IRemoveManifestStorage
    {
        Task RemoveManifestByIdAsync(Guid id, CancellationToken ct);
    }
}
