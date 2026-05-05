using CatalogAPI.Domain.Models;
using CatalogAPI.Domain.Storage.GetVenues;
using CatalogAPI.Domain.UseCases.GetVenues;
using Homework.Ticketing.System.Shared.Models;
using Moq;

namespace CatalogAPI.Tests.UseCases;

public class GetVenuesRequestHandlerTests
{
    private readonly Mock<IGetVenuesStorage> _storageMock = new();
    private readonly GetVenuesRequestHandler _sut;

    public GetVenuesRequestHandlerTests()
    {
        _sut = new GetVenuesRequestHandler(_storageMock.Object);
    }

    [Fact]
    public async Task Handle_DelegatesToStorage_AndReturnsResult()
    {
        var request = new GetVenuesRequest
        {
            Page = 2,
            PageSize = 5,
            SearchName = "hall",
            City = "London",
            Country = "UK"
        };
        var expected = new ResultModel<List<VenueModel>>
        {
            Count = 1,
            Data = [new VenueModel { Name = "Royal Albert Hall" }]
        };
        _storageMock
            .Setup(s => s.GetAsync(
                request.Page, request.PageSize,
                request.SearchName, request.City, request.Country,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        var result = await _sut.Handle(request, default);

        Assert.Same(expected, result);
        _storageMock.VerifyAll();
    }
}
