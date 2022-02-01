using MediatR;
using TranslationService.Domain;
using TranslationService.Domain.Book.V1.List;
using BookDTO = TranslationService.Domain.Book.V1.Book;

namespace TranslationService.Application.Book.V1
{
    public class BookListHandler : BookHandler, IRequestHandler<BookFilter, IEnumerable<BookDTO>>
    {
        public BookListHandler(IRepository<BookDTO, BookFilter> repository)
            : base(repository)
        {
            ;
        }

        Task<IEnumerable<BookDTO>> IRequestHandler<BookFilter, IEnumerable<BookDTO>>.Handle(BookFilter request, CancellationToken cancellationToken) =>
            _repository.GetAllAsync(request);
    }
}
