using CatalogAPI.Core.Models;
using CatalogAPI.DAL.Storage.GetVenues;
using Homework.Ticketing.System.Shared.Models;
using MediatR;

namespace CatalogAPI.Domain.UseCases.GetVenues
{
    public class GetVenuesRequestHandler : IRequestHandler<GetVenuesRequest, ResultModel<List<VenueModel>>>
    {
        private readonly IGetVenuesStorage _storage;

        public GetVenuesRequestHandler(IGetVenuesStorage storage)
        {
            _storage = storage;
        }

        public async Task<ResultModel<List<VenueModel>>> Handle(GetVenuesRequest request, CancellationToken cancellationToken)
        {
            return await _storage.GetAsync(request.Page, request.PageSize, request.SearchName, request.City, request.Country, cancellationToken);
        }
    }
}
