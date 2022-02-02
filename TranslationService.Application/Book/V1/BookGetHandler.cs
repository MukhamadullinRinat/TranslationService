using MediatR;
using TranslationService.Domain;
using TranslationService.Domain.Book.V1.GET;
using TranslationService.Domain.Book.V1.List;
using BookDTO = TranslationService.Domain.Book.V1.Book;

namespace TranslationService.Application.Book.V1
{
    public class BookGetHandler : BookHandler, IRequestHandler<BookRequestGet, BookDTO>
    {
        public BookGetHandler(IBookRepository repository)
            : base(repository)
        {
            ;
        }

        public Task<BookDTO> Handle(BookRequestGet request, CancellationToken cancellationToken) =>
            _repository.GetAsync(request.Guid);
    }
}
