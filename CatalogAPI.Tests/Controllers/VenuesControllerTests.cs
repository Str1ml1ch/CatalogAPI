using CatalogAPI.Controllers;
using CatalogAPI.Domain.Models;
using CatalogAPI.Domain.UseCases.GetSections;
using CatalogAPI.Domain.UseCases.GetVenues;
using Homework.Ticketing.System.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CatalogAPI.Tests.Controllers;

public class VenuesControllerTests
{
    private readonly Mock<IMediator> _mediatorMock = new();
    private readonly VenuesController _sut;

    public VenuesControllerTests()
    {
        _sut = new VenuesController(_mediatorMock.Object);
    }

    [Fact]
    public async Task Get_ReturnsOk_WithResult()
    {
        var expected = new ResultModel<List<VenueModel>>
        {
            Count = 1,
            Data = [new VenueModel { Id = Guid.NewGuid(), Name = "Test Venue" }]
        };
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetVenuesRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        var result = await _sut.Get(cancellationToken: default);

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Same(expected, ok.Value);
    }

    [Fact]
    public async Task Get_SendsCorrectRequest_WithQueryParameters()
    {
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetVenuesRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ResultModel<List<VenueModel>> { Count = 0, Data = [] });

        await _sut.Get(page: 3, pageSize: 20, searchName: "hall", city: "London", country: "UK",
            cancellationToken: default);

        _mediatorMock.Verify(m => m.Send(It.Is<GetVenuesRequest>(r =>
            r.Page == 3 && r.PageSize == 20 && r.SearchName == "hall" &&
            r.City == "London" && r.Country == "UK"),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetSections_ReturnsOk_WithResult()
    {
        var venueId = Guid.NewGuid();
        var expected = new ResultModel<List<SectionModel>> { Count = 0, Data = [] };
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetSectionsRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        var result = await _sut.GetSections(venueId, cancellationToken: default);

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Same(expected, ok.Value);
    }

    [Fact]
    public async Task GetSections_SendsCorrectRequest()
    {
        var venueId = Guid.NewGuid();
        var manifestId = Guid.NewGuid();
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetSectionsRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ResultModel<List<SectionModel>> { Count = 0, Data = [] });

        await _sut.GetSections(venueId, manifestId: manifestId, page: 2, pageSize: 25,
            cancellationToken: default);

        _mediatorMock.Verify(m => m.Send(It.Is<GetSectionsRequest>(r =>
            r.VenueId == venueId && r.ManifestId == manifestId && r.Page == 2 && r.PageSize == 25),
            It.IsAny<CancellationToken>()), Times.Once);
    }
}
