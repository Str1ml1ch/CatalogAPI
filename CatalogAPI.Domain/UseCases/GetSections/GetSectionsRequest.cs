using CatalogAPI.Core.Models;
using Homework.Ticketing.System.Shared.Models;
using MediatR;

namespace CatalogAPI.Domain.UseCases.GetSections
{
    public class GetSectionsRequest : IRequest<ResultModel<List<SectionModel>>>
    {
        public Guid VenueId { get; set; }
        public Guid? ManifestId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }
}
