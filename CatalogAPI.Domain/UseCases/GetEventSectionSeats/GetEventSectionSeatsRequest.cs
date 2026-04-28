using CatalogAPI.Core.Models;
using Homework.Ticketing.System.Shared.Models;
using MediatR;

namespace CatalogAPI.Domain.UseCases.GetEventSectionSeats
{
    public class GetEventSectionSeatsRequest : IRequest<ResultModel<List<SeatDetailModel>>>
    {
        public Guid EventId { get; set; }
        public Guid SectionId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }
}
