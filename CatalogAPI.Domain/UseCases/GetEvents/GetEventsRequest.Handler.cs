using CatalogAPI.Core.Models;
using CatalogAPI.DAL.Storage.GetEvents;
using Homework.Ticketing.System.Shared.Models;
using MediatR;

namespace CatalogAPI.Domain.UseCases.GetEvents
{
    public class GetEventsRequestHandler : IRequestHandler<GetEventsRequest, ResultModel<List<EventModel>>>
    {
        private readonly IGetEventsStorage _storage;

        public GetEventsRequestHandler(IGetEventsStorage storage)
        {
            _storage = storage;
        }

        public async Task<ResultModel<List<EventModel>>> Handle(GetEventsRequest request, CancellationToken cancellationToken)
        {
           return await _storage.GetAsync(
               request.Page, 
               request.PageSize, 
               request.ManifestId, 
               request.SearchName, 
               request.Status, 
               request.FromDate, 
               request.ToDate ,
               cancellationToken);
        }
    }
}
