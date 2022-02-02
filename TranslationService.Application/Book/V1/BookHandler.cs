using TranslationService.Domain;

namespace TranslationService.Application.Book.V1
{
    public class BookHandler
    {
        protected readonly IBookRepository _repository;

        public BookHandler(IBookRepository repository)
        {
            _repository = repository;
        }
    }
}
