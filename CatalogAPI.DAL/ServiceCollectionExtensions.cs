using CatalogAPI.DAL.Storage;
using CatalogAPI.DAL.Storage.CreateEvent;
using CatalogAPI.DAL.Storage.CreateManifest;
using CatalogAPI.DAL.Storage.CreateSeat;
using CatalogAPI.DAL.Storage.GetEventById;
using CatalogAPI.DAL.Storage.GetEventSectionSeats;
using CatalogAPI.DAL.Storage.GetEvents;
using CatalogAPI.DAL.Storage.GetManifestById;
using CatalogAPI.DAL.Storage.GetManifests;
using CatalogAPI.DAL.Storage.GetSections;
using CatalogAPI.DAL.Storage.GetSeats;
using CatalogAPI.DAL.Storage.GetVenue;
using CatalogAPI.DAL.Storage.GetVenues;
using CatalogAPI.DAL.Storage.RemoveEvent;
using CatalogAPI.DAL.Storage.RemoveManifest;
using CatalogAPI.DAL.Storage.RemoveSeat;
using CatalogAPI.DAL.Storage.RemoveVenue;
using CatalogAPI.DAL.Storage.UpdateEvent;
using CatalogAPI.DAL.Storage.UpdateManifest;
using CatalogAPI.DAL.Storage.UpdateVenue;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CatalogAPI.DAL.Storage.GetSectionById;

namespace CatalogAPI.DAL
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddStorage(this IServiceCollection services, string connectionString)
        {
            return services.AddDbContextPool<CatalogDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .AddScoped<ICrateVenueStorage, CreateVenueStorage>()
                .AddScoped<ICreateEventStorage, CreateEventStorage>()
                .AddScoped<ICreateManifestStorage, CreateManifestStorage>()
                .AddScoped<ICreateSeatStorage, CreateSeatStorage>()
                .AddScoped<IGetEventByIdStorage, GetEventByIdStorage>()
                .AddScoped<IGetEventsStorage, GetEventsStorage>()
                .AddScoped<IGetEventSectionSeatsStorage, GetEventSectionSeatsStorage>()
                .AddScoped<IGetManifestByIdStorage, GetManifestByIdStorage>()
                .AddScoped<IGetManifestsStorage, GetManifestsStorage>()
                .AddScoped<IGetSectionsStorage, GetSectionsStorage>()
                .AddScoped<IGetSeatsStorage, GetSeatsStorage>()
                .AddScoped<IGetVenueStorageById, GetVenueStorageById>()
                .AddScoped<IGetVenuesStorage, GetVenuesStorage>()
                .AddScoped<IRemoveEventStorage, RemoveEventStorage>()
                .AddScoped<IRemoveManifestStorage, RemoveManifestStorage>()
                .AddScoped<IRemoveSeatStorage, RemoveSeatStorage>()
                .AddScoped<IRemoveVenueStorage, RemoveVenueStorage>()
                .AddScoped<IUpdateEventStorage, UpdateEventStorage>()
                .AddScoped<IUpdateManifestStorage, UpdateManifestStorage>()
                .AddScoped<IUpdateVenueStorage, UpdateVenueStorage>()
                .AddScoped<IGetSectionByIdStorage, GetSectionByIdStorage>();
        }
    }
}
