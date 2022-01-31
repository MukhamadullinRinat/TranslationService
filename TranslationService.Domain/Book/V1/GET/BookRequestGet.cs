using MediatR;

namespace TranslationService.Domain.Book.V1.GET
{
    public class BookRequestGet : IRequest<Book>
    {
        public Guid Guid { get; set; }
    }
}
