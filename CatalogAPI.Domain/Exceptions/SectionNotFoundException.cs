namespace CatalogAPI.Domain.Exceptions
{
    public class SectionNotFoundException : NotFoundException
    {
        public SectionNotFoundException(Guid id) : base(String.Format("Section with id: {0} not found", id)) { }
    }
}
