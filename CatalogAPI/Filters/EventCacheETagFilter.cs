using CatalogAPI.Domain.UseCases.GetEvents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;

namespace CatalogAPI.Filters;

public sealed class EventCacheETagFilter : IAsyncActionFilter
{
    private readonly IDistributedCache _cache;

    public EventCacheETagFilter(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var version = await _cache.GetStringAsync(GetEventsRequestHandler.CacheVersionKey) ?? "0";
        var etag = $"\"{version}\"";

        var ifNoneMatch = context.HttpContext.Request.Headers.IfNoneMatch.FirstOrDefault();
        if (ifNoneMatch == etag)
        {
            context.Result = new StatusCodeResult(StatusCodes.Status304NotModified);
            return;
        }

        await next();

        context.HttpContext.Response.Headers.ETag = etag;
    }
}
