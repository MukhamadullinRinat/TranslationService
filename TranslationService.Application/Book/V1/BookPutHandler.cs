using MediatR;
using TranslationService.Domain;
using TranslationService.Domain.Book.V1.List;
using TranslationService.Domain.Book.V1.PUT;
using BookDTO = TranslationService.Domain.Book.V1.Book;

namespace TranslationService.Application.Book.V1
{
    public class BookPutHandler : BookHandler, IRequestHandler<BookRequestPut, BookDTO>
    {
        public BookPutHandler(IRepository<BookDTO, BookFilter> repository)
            : base(repository)
        {
            ;
        }

        public async Task<BookDTO> Handle(BookRequestPut request, CancellationToken cancellationToken)
        {
            var book = await _repository.GetAsync(request.Guid);

            book.Title = String.IsNullOrEmpty(request.Title) ? book.Title : request.Title;
            book.PageNumber = request.PageNumber.HasValue ? request.PageNumber.Value : book.PageNumber;

            await _repository.UpdateAsync(book);

            return await _repository.GetAsync(request.Guid);
        }
    }
}
