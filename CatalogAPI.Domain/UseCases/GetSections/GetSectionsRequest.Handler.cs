using CatalogAPI.Core.Models;
using CatalogAPI.DAL.Storage.GetSections;
using CatalogAPI.DAL.Storage.GetVenue;
using CatalogAPI.Domain.Exceptions;
using Homework.Ticketing.System.Shared.Models;
using MediatR;

namespace CatalogAPI.Domain.UseCases.GetSections
{
    public class GetSectionsRequestHandler : IRequestHandler<GetSectionsRequest, ResultModel<List<SectionModel>>>
    {
        private readonly IGetSectionsStorage _storage;
        private readonly IGetVenueStorageById _getVenueStorageById;

        public GetSectionsRequestHandler(IGetSectionsStorage storage, IGetVenueStorageById getVenueStorageById)
        {
            _storage = storage;
            _getVenueStorageById = getVenueStorageById;
        }

        public async Task<ResultModel<List<SectionModel>>> Handle(GetSectionsRequest request, CancellationToken cancellationToken)
        {
            var isVenueExist = await _getVenueStorageById.IsVenueByIdExistAsync(request.VenueId, cancellationToken);

            if(!isVenueExist)
            {
                throw new VenueNotFoundException(request.VenueId);
            }

            return await _storage.GetByVenueAsync(request.VenueId, request.ManifestId, request.Page, request.PageSize, cancellationToken);
        }
    }
}
