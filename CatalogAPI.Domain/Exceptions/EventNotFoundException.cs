namespace CatalogAPI.Domain.Exceptions
{
    public class EventNotFoundException : NotFoundException
    {
        public EventNotFoundException(Guid id) : base(String.Format("Event with id: {0} not found", id))
        {

        }
    }
}
