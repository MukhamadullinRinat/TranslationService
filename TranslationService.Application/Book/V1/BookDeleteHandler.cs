using MediatR;
using TranslationService.Domain;
using TranslationService.Domain.Book.V1.DELETE;
using TranslationService.Domain.Book.V1.List;
using BookDTO = TranslationService.Domain.Book.V1.Book;

namespace TranslationService.Application.Book.V1
{
    public class BookDeleteHandler : BookHandler, IRequestHandler<BookRequestDelete, Guid>
    {
        public BookDeleteHandler(IRepository<BookDTO, BookFilter> repository)
            : base(repository)
        {
            ;
        }

        public Task<Guid> Handle(BookRequestDelete request, CancellationToken cancellationToken) =>
            _repository.DeleteAsync(request.Guid);
    }
}
