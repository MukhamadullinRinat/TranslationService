using MediatR;
using TranslationService.Domain;
using TranslationService.Domain.Book.V1.GET;
using TranslationService.Domain.Book.V1.List;
using BookDTO = TranslationService.Domain.Book.V1.Book;

namespace TranslationService.Application.Book.V1
{
    public class BookGetHandler : IRequestHandler<BookRequestGet, BookDTO>
    {
        private readonly IRepository<BookDTO, BookFilter> _repository;

        public BookGetHandler(IRepository<BookDTO, BookFilter> repository)
        {
            _repository = repository;
        }

        public Task<BookDTO> Handle(BookRequestGet request, CancellationToken cancellationToken) =>
            _repository.GetAsync(request.Guid);
    }
}
