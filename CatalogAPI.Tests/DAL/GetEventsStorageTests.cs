using CatalogAPI.DAL;
using CatalogAPI.DAL.Entities;
using CatalogAPI.DAL.Storage.GetEvents;
using CatalogAPI.Domain.Enums;
using CatalogAPI.Domain.Storage.GetEvents;
using CatalogAPI.Tests.DAL.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CatalogAPI.Tests.DAL;

[Collection("SqlServer")]
public class GetEventsStorageTests : IAsyncLifetime
{
    private readonly SqlServerContainerFixture _fixture;
    private CatalogDbContext _context = null!;
    private IDbContextTransaction _transaction = null!;
    private GetEventsStorage _sut = null!;

    public GetEventsStorageTests(SqlServerContainerFixture fixture) => _fixture = fixture;

    public async Task InitializeAsync()
    {
        _context = new CatalogDbContext(
            new DbContextOptionsBuilder<CatalogDbContext>()
                .UseSqlServer(_fixture.ConnectionString)
                .Options);
        _transaction = await _context.Database.BeginTransactionAsync();
        _sut = new GetEventsStorage(_context);
    }

    public async Task DisposeAsync()
    {
        await _transaction.RollbackAsync();
        await _context.DisposeAsync();
    }

    private Guid SeedManifest()
    {
        var venue = new Venue { Id = Guid.NewGuid(), Name = "Test Venue", CreatedAt = DateTimeOffset.UtcNow };
        _context.Venues.Add(venue);
        var manifest = new SeatManifest { Id = Guid.NewGuid(), VenueId = venue.Id, Name = "Manifest", Capacity = 100, CreatedAt = DateTimeOffset.UtcNow };
        _context.SeatManifests.Add(manifest);
        _context.SaveChanges();
        return manifest.Id;
    }

    private Event CreateEvent(string name, EEventStatus status = EEventStatus.Published,
        DateTimeOffset? startDate = null, DateTimeOffset? endDate = null, Guid? manifestId = null)
        => new()
        {
            Id = Guid.NewGuid(),
            ManifestId = manifestId ?? SeedManifest(),
            Name = name,
            StartDate = startDate ?? DateTimeOffset.UtcNow,
            EndDate = endDate ?? DateTimeOffset.UtcNow.AddDays(1),
            Status = status,
            CreatedAt = DateTimeOffset.UtcNow
        };

    [Fact]
    public async Task GetAsync_ReturnsAllEvents_WhenNoFiltersApplied()
    {
        _context.Events.AddRange(
            CreateEvent("Event A"),
            CreateEvent("Event B", EEventStatus.Draft));
        await _context.SaveChangesAsync();

        var result = await _sut.GetAsync(1, 10, null, null, null, null, null, default);

        Assert.Equal(2, result.Count);
        Assert.Equal(2, result.Data!.Count);
    }

    [Fact]
    public async Task GetAsync_FiltersByManifestId()
    {
        var targetManifestId = SeedManifest();
        _context.Events.AddRange(
            CreateEvent("Matching", manifestId: targetManifestId),
            CreateEvent("Non-Matching"));
        await _context.SaveChangesAsync();

        var result = await _sut.GetAsync(1, 10, targetManifestId, null, null, null, null, default);

        Assert.Equal(1, result.Count);
        Assert.Single(result.Data!);
        Assert.Equal("Matching", result.Data![0].Name);
    }

    [Fact]
    public async Task GetAsync_FiltersBySearchName()
    {
        _context.Events.AddRange(
            CreateEvent("Rock Concert"),
            CreateEvent("Jazz Night"));
        await _context.SaveChangesAsync();

        var result = await _sut.GetAsync(1, 10, null, "Rock", null, null, null, default);

        Assert.Equal(1, result.Count);
        Assert.Equal("Rock Concert", result.Data![0].Name);
    }

    [Fact]
    public async Task GetAsync_FiltersByStatus()
    {
        _context.Events.AddRange(
            CreateEvent("Published Event", EEventStatus.Published),
            CreateEvent("Draft Event", EEventStatus.Draft));
        await _context.SaveChangesAsync();

        var result = await _sut.GetAsync(1, 10, null, null, EEventStatus.Published, null, null, default);

        Assert.Equal(1, result.Count);
        Assert.Equal("Published Event", result.Data![0].Name);
    }

    [Fact]
    public async Task GetAsync_FiltersByDateRange()
    {
        var fromDate = DateTimeOffset.UtcNow.AddDays(5);
        var toDate = DateTimeOffset.UtcNow.AddDays(10);
        _context.Events.AddRange(
            CreateEvent("In Range",
                startDate: DateTimeOffset.UtcNow.AddDays(6),
                endDate: DateTimeOffset.UtcNow.AddDays(9)),
            CreateEvent("Out of Range",
                startDate: DateTimeOffset.UtcNow,
                endDate: DateTimeOffset.UtcNow.AddDays(1)));
        await _context.SaveChangesAsync();

        var result = await _sut.GetAsync(1, 10, null, null, null, fromDate, toDate, default);

        Assert.Equal(1, result.Count);
        Assert.Equal("In Range", result.Data![0].Name);
    }

    [Fact]
    public async Task GetAsync_PaginatesCorrectly()
    {
        for (int i = 0; i < 5; i++)
            _context.Events.Add(CreateEvent($"Event {i}", startDate: DateTimeOffset.UtcNow.AddDays(i)));
        await _context.SaveChangesAsync();

        var page2 = await _sut.GetAsync(2, 2, null, null, null, null, null, default);

        Assert.Equal(5, page2.Count);
        Assert.Equal(2, page2.Data!.Count);
    }

    [Fact]
    public async Task GetAsync_ReturnsEmpty_WhenNoMatchFound()
    {
        var result = await _sut.GetAsync(1, 10, Guid.NewGuid(), null, null, null, null, default);

        Assert.Equal(0, result.Count);
        Assert.Empty(result.Data!);
    }
}
