namespace CatalogAPI.Domain.Exceptions
{
    public class VenueNotFoundException : NotFoundException
    {
        public VenueNotFoundException(Guid id) : base(String.Format("Forum with Id {0} not found", id)) 
        {
        }
    }
}
