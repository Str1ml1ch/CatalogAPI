namespace CatalogAPI.DAL.Storage.RemoveSeat
{
    public interface IRemoveSeatStorage
    {
        Task RemoveSeatByIdAsync(Guid id, CancellationToken ct);
        Task RemoveAllByManifestIdAsync(Guid manifestId, CancellationToken ct);
    }
}
