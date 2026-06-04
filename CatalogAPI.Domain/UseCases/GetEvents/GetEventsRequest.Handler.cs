using CatalogAPI.Domain.Models;
using CatalogAPI.Domain.Storage.GetEvents;
using Homework.Ticketing.System.Shared.Models;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace CatalogAPI.Domain.UseCases.GetEvents
{
    public class GetEventsRequestHandler : IRequestHandler<GetEventsRequest, ResultModel<List<EventModel>>>
    {
        public const string CacheVersionKey = "events:cache:version";
        private const int CacheTtlMinutes = 5;

        private readonly IGetEventsStorage _storage;
        private readonly IDistributedCache _cache;

        public GetEventsRequestHandler(IGetEventsStorage storage, IDistributedCache cache)
        {
            _storage = storage;
            _cache = cache;
        }

        public async Task<ResultModel<List<EventModel>>> Handle(GetEventsRequest request, CancellationToken cancellationToken)
        {
            var version = await _cache.GetStringAsync(CacheVersionKey, cancellationToken) ?? "0";
            var cacheKey = BuildCacheKey(version, request);

            var cached = await _cache.GetStringAsync(cacheKey, cancellationToken);
            if (cached is not null)
                return JsonSerializer.Deserialize<ResultModel<List<EventModel>>>(cached)!;

            var result = await _storage.GetAsync(
                request.Page,
                request.PageSize,
                request.ManifestId,
                request.SearchName,
                request.Status,
                request.FromDate,
                request.ToDate,
                cancellationToken);

            var serialized = JsonSerializer.Serialize(result);
            await _cache.SetStringAsync(cacheKey, serialized, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(CacheTtlMinutes)
            }, cancellationToken);

            return result;
        }

        private static string BuildCacheKey(string version, GetEventsRequest request)
            => $"events:v{version}:p{request.Page}:ps{request.PageSize}:m{request.ManifestId}:s{request.SearchName}:st{request.Status}:fd{request.FromDate:O}:td{request.ToDate:O}";
    }
}
