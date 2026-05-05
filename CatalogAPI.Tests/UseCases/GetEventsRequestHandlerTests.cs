using CatalogAPI.Domain.Enums;
using CatalogAPI.Domain.Models;
using CatalogAPI.Domain.Storage.GetEvents;
using CatalogAPI.Domain.UseCases.GetEvents;
using Homework.Ticketing.System.Shared.Models;
using Moq;

namespace CatalogAPI.Tests.UseCases;

public class GetEventsRequestHandlerTests
{
    private readonly Mock<IGetEventsStorage> _storageMock = new();
    private readonly GetEventsRequestHandler _sut;

    public GetEventsRequestHandlerTests()
    {
        _sut = new GetEventsRequestHandler(_storageMock.Object);
    }

    [Fact]
    public async Task Handle_DelegatesToStorage_AndReturnsResult()
    {
        var request = new GetEventsRequest
        {
            Page = 1,
            PageSize = 10,
            ManifestId = Guid.NewGuid(),
            SearchName = "test",
            Status = EEventStatus.Published,
            FromDate = DateTimeOffset.UtcNow,
            ToDate = DateTimeOffset.UtcNow.AddDays(7)
        };
        var expected = new ResultModel<List<EventModel>>
        {
            Count = 2,
            Data = [new EventModel { Name = "Event 1" }, new EventModel { Name = "Event 2" }]
        };
        _storageMock
            .Setup(s => s.GetAsync(
                request.Page, request.PageSize, request.ManifestId,
                request.SearchName, request.Status,
                request.FromDate, request.ToDate,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        var result = await _sut.Handle(request, default);

        Assert.Same(expected, result);
        _storageMock.VerifyAll();
    }
}
