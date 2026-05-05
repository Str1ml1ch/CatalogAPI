namespace CatalogAPI.Domain.Storage
{
    public interface ICrateVenueStorage
    {
        Task CreateAsync(string name, string? description, string? address, string? city, string? country, string? postalCode, CancellationToken ct);
    }
}
