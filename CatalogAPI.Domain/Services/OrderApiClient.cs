using System.Net.Http.Json;

namespace CatalogAPI.Domain.Services
{
    public class OrderApiClient : IOrderApiClient
    {
        private readonly IHttpClientFactory _factory;

        public OrderApiClient(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task<Dictionary<Guid, int>> GetSeatStatusesAsync(IEnumerable<Guid> seatIds, CancellationToken ct)
        {
            var client = _factory.CreateClient("OrderApi");
            var query = string.Join("&", seatIds.Select(id => $"seatIds={id}"));
            var result = await client.GetFromJsonAsync<Dictionary<Guid, int>>(
                $"/api/orders/seats/statuses?{query}", ct);
            return result ?? new Dictionary<Guid, int>();
        }
    }
}
