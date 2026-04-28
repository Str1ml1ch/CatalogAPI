using CatalogAPI.Domain.UseCases.GetSections;
using CatalogAPI.Domain.UseCases.GetVenues;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Controllers
{
    [Route("api/[controller]")]
    public class VenuesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VenuesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page = 1, 
            [FromQuery] int pageSize = 10, 
            [FromQuery] string? searchName = null, 
            [FromQuery] string? city = null, 
            [FromQuery] string? country = null,
            CancellationToken cancellationToken = default)
        {
            var data = await _mediator.Send(new GetVenuesRequest
            {
                Page = page,
                PageSize = pageSize,
                City = city,
                Country = country,
                SearchName = searchName
            }, cancellationToken);
            return Ok(data);
        }

        [HttpGet("{venue_id}/sections")]
        public async Task<IActionResult> GetSections(
            Guid venue_id,
            [FromQuery] Guid? manifestId = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 50,
            CancellationToken cancellationToken = default)
        {
            var data = await _mediator.Send(new GetSectionsRequest
            {
                VenueId = venue_id,
                ManifestId = manifestId,
                Page = page,
                PageSize = pageSize
            }, cancellationToken);
            return Ok(data);
        }
    }
}
