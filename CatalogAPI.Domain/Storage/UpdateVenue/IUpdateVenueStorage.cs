namespace CatalogAPI.Domain.Storage.UpdateVenue
{
    public interface IUpdateVenueStorage
    {
        Task UpdateAsync(Guid id, string name, string? description, string? address, string? city, string? country, string? postalCode, CancellationToken ct);
    }
}
