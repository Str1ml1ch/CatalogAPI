using CatalogAPI.Core.Enums;
using CatalogAPI.Core.Models;
using Homework.Ticketing.System.Shared.Models;
using MediatR;

namespace CatalogAPI.Domain.UseCases.GetEvents
{
    public class GetEventsRequest : IRequest<ResultModel<List<EventModel>>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public Guid? ManifestId { get; set; }
        public string? SearchName { get; set; }
        public EEventStatus? Status { get; set; }
        public DateTimeOffset? FromDate { get; set; }
        public DateTimeOffset? ToDate { get; set; }
    }
}
