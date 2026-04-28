namespace CatalogAPI.DAL.Storage.GetSectionById
{
    public interface IGetSectionByIdStorage
    {
        Task<bool> IsSectionByIdExistAsync(Guid id, CancellationToken ct);
    }
}
