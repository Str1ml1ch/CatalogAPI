using CatalogAPI.Core.Enums;
using CatalogAPI.Core.Models;
using CatalogAPI.DAL.Storage.GetEventById;
using CatalogAPI.DAL.Storage.GetEventSectionSeats;
using CatalogAPI.DAL.Storage.GetManifestById;
using CatalogAPI.DAL.Storage.GetSectionById;
using CatalogAPI.Domain.Exceptions;
using CatalogAPI.Domain.Services;
using Homework.Ticketing.System.Shared.Models;
using MediatR;

namespace CatalogAPI.Domain.UseCases.GetEventSectionSeats
{
    public class GetEventSectionSeatsRequestHandler : IRequestHandler<GetEventSectionSeatsRequest, ResultModel<List<SeatDetailModel>>>
    {
        private readonly IGetEventSectionSeatsStorage _storage;
        private readonly IGetEventByIdStorage _eventByIdStorage;
        private readonly IGetSectionByIdStorage _sectionByIdStorage;
        private readonly IOrderApiClient _orderClient;

        public GetEventSectionSeatsRequestHandler(IGetEventSectionSeatsStorage storage, IOrderApiClient orderClient, IGetEventByIdStorage eventByIdStorage, IGetSectionByIdStorage sectionByIdStorage)
        {
            _storage = storage;
            _orderClient = orderClient;
            _eventByIdStorage = eventByIdStorage;
            _sectionByIdStorage = sectionByIdStorage;
        }

        public async Task<ResultModel<List<SeatDetailModel>>> Handle(GetEventSectionSeatsRequest request, CancellationToken cancellationToken)
        {
            var isEventExist = await _eventByIdStorage.IsEventByIdExistAsync(request.EventId, cancellationToken);

            if(!isEventExist)
            {
                throw new EventNotFoundException(request.EventId);
            }

            var isSectionExist = await _sectionByIdStorage.IsSectionByIdExistAsync(request.SectionId, cancellationToken);

            if(!isSectionExist)
            {
                throw new SectionNotFoundException(request.SectionId);
            }

            var result = await _storage.GetAsync(request.EventId, request.SectionId, request.Page, request.PageSize, cancellationToken);

            if (result.Data is { Count: > 0 })
            {
                var statuses = await _orderClient.GetSeatStatusesAsync(result.Data.Select(s => s.Id), cancellationToken);
                foreach (var seat in result.Data)
                {
                    if (statuses.TryGetValue(seat.Id, out var statusInt))
                        seat.Status = (ESeatStatus)statusInt;
                }
            }

            return result;
        }
    }
}
