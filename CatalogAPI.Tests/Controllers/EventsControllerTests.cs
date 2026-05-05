using CatalogAPI.Controllers;
using CatalogAPI.Domain.Models;
using CatalogAPI.Domain.UseCases.GetEventSectionSeats;
using CatalogAPI.Domain.UseCases.GetEvents;
using Homework.Ticketing.System.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CatalogAPI.Tests.Controllers;

public class EventsControllerTests
{
    private readonly Mock<IMediator> _mediatorMock = new();
    private readonly EventsController _sut;

    public EventsControllerTests()
    {
        _sut = new EventsController(_mediatorMock.Object);
    }

    [Fact]
    public async Task Get_ReturnsOk_WithResult()
    {
        var expected = new ResultModel<List<EventModel>>
        {
            Count = 1,
            Data = [new EventModel { Id = Guid.NewGuid(), Name = "Test Event" }]
        };
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetEventsRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        var result = await _sut.Get(cancellationToken: default);

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Same(expected, ok.Value);
    }

    [Fact]
    public async Task Get_SendsCorrectRequest_WithQueryParameters()
    {
        var manifestId = Guid.NewGuid();
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetEventsRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ResultModel<List<EventModel>> { Count = 0, Data = [] });

        await _sut.Get(page: 2, pageSize: 5, manifestId: manifestId, searchName: "rock",
            cancellationToken: default);

        _mediatorMock.Verify(m => m.Send(It.Is<GetEventsRequest>(r =>
            r.Page == 2 && r.PageSize == 5 && r.ManifestId == manifestId && r.SearchName == "rock"),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetSeats_ReturnsOk_WithResult()
    {
        var eventId = Guid.NewGuid();
        var sectionId = Guid.NewGuid();
        var expected = new ResultModel<List<SeatDetailModel>> { Count = 0, Data = [] };
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetEventSectionSeatsRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        var result = await _sut.GetSeats(eventId, sectionId, cancellationToken: default);

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Same(expected, ok.Value);
    }

    [Fact]
    public async Task GetSeats_SendsCorrectRequest()
    {
        var eventId = Guid.NewGuid();
        var sectionId = Guid.NewGuid();
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetEventSectionSeatsRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ResultModel<List<SeatDetailModel>> { Count = 0, Data = [] });

        await _sut.GetSeats(eventId, sectionId, page: 2, pageSize: 25, cancellationToken: default);

        _mediatorMock.Verify(m => m.Send(It.Is<GetEventSectionSeatsRequest>(r =>
            r.EventId == eventId && r.SectionId == sectionId && r.Page == 2 && r.PageSize == 25),
            It.IsAny<CancellationToken>()), Times.Once);
    }
}
