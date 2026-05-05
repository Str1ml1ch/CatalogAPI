namespace CatalogAPI.Domain.Storage.UpdateManifest
{
    public interface IUpdateManifestStorage
    {
        Task UpdateAsync(Guid id, string name, int capacity, CancellationToken ct);
    }
}