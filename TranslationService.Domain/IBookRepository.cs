using TranslationService.Domain.Book;
using TranslationService.Domain.Book.V1.List;
using BookDTO = TranslationService.Domain.Book.V1.Book;

namespace TranslationService.Domain
{
    public interface IBookRepository : IRepository<BookDTO, BookCreateModel, BookFilter>
    {
        Task<FileStream> GetStreamAsync(Guid guid);
    }
}
