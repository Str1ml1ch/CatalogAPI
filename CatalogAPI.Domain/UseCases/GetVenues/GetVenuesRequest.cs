using CatalogAPI.Core.Models;
using Homework.Ticketing.System.Shared.Models;
using MediatR;

namespace CatalogAPI.Domain.UseCases.GetVenues
{
    public class GetVenuesRequest : IRequest<ResultModel<List<VenueModel>>>
    {
        public int Page { get; set; } = 0;
        public int PageSize { get; set; } = 0;
        public string? SearchName { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
    }
}
