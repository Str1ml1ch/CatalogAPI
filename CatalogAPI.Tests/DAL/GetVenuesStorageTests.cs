using CatalogAPI.DAL;
using CatalogAPI.DAL.Entities;
using CatalogAPI.DAL.Storage.GetVenues;
using CatalogAPI.Domain.Storage.GetVenues;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Tests.DAL;

public class GetVenuesStorageTests : IDisposable
{
    private readonly CatalogDbContext _context;
    private readonly GetVenuesStorage _sut;

    public GetVenuesStorageTests()
    {
        var options = new DbContextOptionsBuilder<CatalogDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new CatalogDbContext(options);
        _sut = new GetVenuesStorage(_context);
    }

    public void Dispose() => _context.Dispose();

    private Venue CreateVenue(string name, string? city = null, string? country = null)
        => new()
        {
            Id = Guid.NewGuid(),
            Name = name,
            City = city,
            Country = country,
            CreatedAt = DateTimeOffset.UtcNow
        };

    [Fact]
    public async Task GetAsync_ReturnsAllVenues_WhenNoFiltersApplied()
    {
        _context.Venues.AddRange(
            CreateVenue("Venue A"),
            CreateVenue("Venue B"));
        await _context.SaveChangesAsync();

        var result = await _sut.GetAsync(1, 10, null, null, null, default);

        Assert.Equal(2, result.Count);
        Assert.Equal(2, result.Data!.Count);
    }

    [Fact]
    public async Task GetAsync_FiltersByName()
    {
        _context.Venues.AddRange(
            CreateVenue("Madison Square Garden"),
            CreateVenue("Royal Albert Hall"));
        await _context.SaveChangesAsync();

        var result = await _sut.GetAsync(1, 10, "Madison", null, null, default);

        Assert.Equal(1, result.Count);
        Assert.Equal("Madison Square Garden", result.Data![0].Name);
    }

    [Fact]
    public async Task GetAsync_FiltersByCity()
    {
        _context.Venues.AddRange(
            CreateVenue("Venue NY", city: "New York"),
            CreateVenue("Venue London", city: "London"));
        await _context.SaveChangesAsync();

        var result = await _sut.GetAsync(1, 10, null, "New York", null, default);

        Assert.Equal(1, result.Count);
        Assert.Equal("Venue NY", result.Data![0].Name);
    }

    [Fact]
    public async Task GetAsync_FiltersByCountry()
    {
        _context.Venues.AddRange(
            CreateVenue("Venue US", country: "USA"),
            CreateVenue("Venue UK", country: "UK"));
        await _context.SaveChangesAsync();

        var result = await _sut.GetAsync(1, 10, null, null, "USA", default);

        Assert.Equal(1, result.Count);
        Assert.Equal("Venue US", result.Data![0].Name);
    }

    [Fact]
    public async Task GetAsync_PaginatesCorrectly()
    {
        for (int i = 0; i < 5; i++)
            _context.Venues.Add(CreateVenue($"Venue {i:D2}"));
        await _context.SaveChangesAsync();

        var page2 = await _sut.GetAsync(2, 2, null, null, null, default);

        Assert.Equal(5, page2.Count);
        Assert.Equal(2, page2.Data!.Count);
    }

    [Fact]
    public async Task GetAsync_ReturnsEmpty_WhenNoMatchFound()
    {
        var result = await _sut.GetAsync(1, 10, null, "NonExistentCity", null, default);

        Assert.Equal(0, result.Count);
        Assert.Empty(result.Data!);
    }
}
