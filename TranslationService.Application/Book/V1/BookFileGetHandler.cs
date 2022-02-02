using MediatR;
using TranslationService.Domain;
using TranslationService.Domain.Book.V1.File;

namespace TranslationService.Application.Book.V1
{
    public class BookFileGetHandler : BookHandler, IRequestHandler<BookRequestFileGet, BookFileResponse>
    {
        public BookFileGetHandler(IBookRepository bookRepository)
            : base(bookRepository)
        {
            ;
        }

        public async Task<BookFileResponse> Handle(BookRequestFileGet request, CancellationToken cancellationToken)
        {
            var book = await _repository.GetAsync(request.Guid);
            var fileStream = await _repository.GetStreamAsync(request.Guid);

            return new BookFileResponse
            {
                ContentType = MimeTypeMap.List.MimeTypeMap.GetMimeType(book.Extention).FirstOrDefault(),
                File = fileStream,
                Name = book.Title
            };
        }
    }
}
