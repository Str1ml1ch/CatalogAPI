namespace CatalogAPI.Domain.Storage.CreateManifest
{
    public interface ICreateManifestStorage
    {
        Task CreateAsync(Guid venueId, string name, int capacity, CancellationToken ct);
    }
}