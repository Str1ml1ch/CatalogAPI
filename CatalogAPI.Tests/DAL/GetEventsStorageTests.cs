using CatalogAPI.Domain.Enums;
using CatalogAPI.DAL;
using CatalogAPI.DAL.Entities;
using CatalogAPI.DAL.Storage.GetEvents;
using CatalogAPI.Domain.Storage.GetEvents;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Tests.DAL;

public class GetEventsStorageTests : IDisposable
{
    private readonly CatalogDbContext _context;
    private readonly GetEventsStorage _sut;

    public GetEventsStorageTests()
    {
        var options = new DbContextOptionsBuilder<CatalogDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new CatalogDbContext(options);
        _sut = new GetEventsStorage(_context);
    }

    public void Dispose() => _context.Dispose();

    private Event CreateEvent(string name, EEventStatus status = EEventStatus.Published,
        DateTimeOffset? startDate = null, DateTimeOffset? endDate = null, Guid? manifestId = null)
        => new()
        {
            Id = Guid.NewGuid(),
            ManifestId = manifestId ?? Guid.NewGuid(),
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
        var targetManifestId = Guid.NewGuid();
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
