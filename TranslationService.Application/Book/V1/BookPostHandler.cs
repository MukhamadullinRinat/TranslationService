using MediatR;
using TranslationService.Domain.Book.V1.POST;
using TranslationService.Domain;
using BookDTO = TranslationService.Domain.Book.V1.Book;
using TranslationService.Domain.Book;

namespace TranslationService.Application.Book.V1
{
    public class BookPostHandler : BookHandler, IRequestHandler<BookRequestPost, BookDTO>
    {
        public BookPostHandler(IBookRepository repository)
            : base(repository)
        {
            ;
        }

        public async Task<BookDTO> Handle(BookRequestPost request, CancellationToken cancellationToken)
        {
            var createModel = new BookCreateModel { FileType = request.FileType, Title = request.Title };

            if (createModel.IsTextType)
            {
                using(var streamReader = new StreamReader(request.Stream))
                {
                    createModel.Content = streamReader.ReadToEnd();
                }
            }

            createModel.Stream = request.Stream;

            var guid = await _repository.CreateAsync(createModel);

            return await _repository.GetAsync(guid);
        }
    }
}
