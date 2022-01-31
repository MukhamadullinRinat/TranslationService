using MediatR;
using TranslationService.Domain;
using TranslationService.Domain.Book.V1.DELETE;
using TranslationService.Domain.Book.V1.List;
using BookDTO = TranslationService.Domain.Book.V1.Book;

namespace TranslationService.Application.Book.V1
{
    public class BookDeleteHandler : IRequestHandler<BookRequestDelete, Guid>
    {
        private readonly IRepository<BookDTO, BookFilter> _repository;

        public BookDeleteHandler(IRepository<BookDTO, BookFilter> repository)
        {
            _repository = repository;
        }

        public Task<Guid> Handle(BookRequestDelete request, CancellationToken cancellationToken) =>
            _repository.DeleteAsync(request.Guid);
    }
}
