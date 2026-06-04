using CatalogAPI.DAL;
using CatalogAPI.DAL.Entities;
using CatalogAPI.DAL.Storage.GetVenues;
using CatalogAPI.Domain.Storage.GetVenues;
using CatalogAPI.Tests.DAL.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CatalogAPI.Tests.DAL;

[Collection("SqlServer")]
public class GetVenuesStorageTests : IAsyncLifetime
{
    private readonly SqlServerContainerFixture _fixture;
    private CatalogDbContext _context = null!;
    private IDbContextTransaction _transaction = null!;
    private GetVenuesStorage _sut = null!;

    public GetVenuesStorageTests(SqlServerContainerFixture fixture) => _fixture = fixture;

    public async Task InitializeAsync()
    {
        _context = new CatalogDbContext(
            new DbContextOptionsBuilder<CatalogDbContext>()
                .UseSqlServer(_fixture.ConnectionString)
                .Options);
        _transaction = await _context.Database.BeginTransactionAsync();
        _sut = new GetVenuesStorage(_context);
    }

    public async Task DisposeAsync()
    {
        await _transaction.RollbackAsync();
        await _context.DisposeAsync();
    }


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
