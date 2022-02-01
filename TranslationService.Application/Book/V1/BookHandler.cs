using TranslationService.Domain;
using TranslationService.Domain.Book.V1.List;
using BookDTO = TranslationService.Domain.Book.V1.Book;

namespace TranslationService.Application.Book.V1
{
    public class BookHandler
    {
        protected readonly IRepository<BookDTO, BookFilter> _repository;

        public BookHandler(IRepository<BookDTO, BookFilter> repository)
        {
            _repository = repository;
        }
    }
}
