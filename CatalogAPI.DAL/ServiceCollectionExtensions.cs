using CatalogAPI.DAL.Storage;
using CatalogAPI.Domain.Storage;
using CatalogAPI.DAL.Storage.CreateEvent;
using CatalogAPI.Domain.Storage.CreateEvent;
using CatalogAPI.DAL.Storage.CreateManifest;
using CatalogAPI.Domain.Storage.CreateManifest;
using CatalogAPI.DAL.Storage.CreateSeat;
using CatalogAPI.Domain.Storage.CreateSeat;
using CatalogAPI.DAL.Storage.GetEventById;
using CatalogAPI.Domain.Storage.GetEventById;
using CatalogAPI.DAL.Storage.GetEventSectionSeats;
using CatalogAPI.Domain.Storage.GetEventSectionSeats;
using CatalogAPI.DAL.Storage.GetEvents;
using CatalogAPI.Domain.Storage.GetEvents;
using CatalogAPI.DAL.Storage.GetManifestById;
using CatalogAPI.Domain.Storage.GetManifestById;
using CatalogAPI.DAL.Storage.GetManifests;
using CatalogAPI.Domain.Storage.GetManifests;
using CatalogAPI.DAL.Storage.GetSections;
using CatalogAPI.Domain.Storage.GetSections;
using CatalogAPI.DAL.Storage.GetSeats;
using CatalogAPI.Domain.Storage.GetSeats;
using CatalogAPI.DAL.Storage.GetVenue;
using CatalogAPI.Domain.Storage.GetVenue;
using CatalogAPI.DAL.Storage.GetVenues;
using CatalogAPI.Domain.Storage.GetVenues;
using CatalogAPI.DAL.Storage.RemoveEvent;
using CatalogAPI.Domain.Storage.RemoveEvent;
using CatalogAPI.DAL.Storage.RemoveManifest;
using CatalogAPI.Domain.Storage.RemoveManifest;
using CatalogAPI.DAL.Storage.RemoveSeat;
using CatalogAPI.Domain.Storage.RemoveSeat;
using CatalogAPI.DAL.Storage.RemoveVenue;
using CatalogAPI.Domain.Storage.RemoveVenue;
using CatalogAPI.DAL.Storage.UpdateEvent;
using CatalogAPI.Domain.Storage.UpdateEvent;
using CatalogAPI.DAL.Storage.UpdateManifest;
using CatalogAPI.Domain.Storage.UpdateManifest;
using CatalogAPI.DAL.Storage.UpdateVenue;
using CatalogAPI.Domain.Storage.UpdateVenue;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CatalogAPI.DAL.Storage.GetSectionById;
using CatalogAPI.Domain.Storage.GetSectionById;

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
