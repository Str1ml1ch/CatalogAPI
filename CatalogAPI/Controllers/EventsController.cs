using CatalogAPI.Core.Enums;
using CatalogAPI.Domain.UseCases.GetEventSectionSeats;
using CatalogAPI.Domain.UseCases.GetEvents;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page = 1, 
            [FromQuery] int pageSize = 10, 
            [FromQuery] Guid? manifestId = null,
            [FromQuery] string? searchName = null, 
            [FromQuery] EEventStatus? status = null, 
            [FromQuery] DateTimeOffset? fromDate = null,
            [FromQuery] DateTimeOffset? toDate = null,
            CancellationToken cancellationToken = default)
        {
            var model = new GetEventsRequest             
            {
                Page = page,
                PageSize = pageSize,
                ManifestId = manifestId,
                SearchName = searchName,
                Status = status,
                FromDate = fromDate,
                ToDate = toDate
            };

            return Ok(await _mediator.Send(model, cancellationToken));
        }

        [HttpGet("{event_id}/sections/{section_id}/seats")]
        public async Task<IActionResult> GetSeats(
            Guid event_id,
            Guid section_id,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 50,
            CancellationToken cancellationToken = default)
        {
            var data = await _mediator.Send(new GetEventSectionSeatsRequest
            {
                EventId = event_id,
                SectionId = section_id,
                Page = page,
                PageSize = pageSize
            }, cancellationToken);
            return Ok(data);
        }
    }
}
