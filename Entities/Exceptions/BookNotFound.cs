namespace Entities.Exceptions
{
    public abstract partial class NotFound
    {
        public sealed class BookNotFound : NotFound
        {
            public BookNotFound(int id) : base($"The book with id : {id} could not found.")
            {
            }
        }
    }
}
