using MediatR;

namespace TranslationService.Domain.Book.V1.File
{
    public class BookRequestFileGet : IRequest<BookFileResponse>
    {
        public Guid Guid { get; set; }
    }
}
