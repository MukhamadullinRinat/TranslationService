using MediatR;
using TranslationService.Domain.Book.V1.POST;
using TranslationService.Domain;
using BookDTO = TranslationService.Domain.Book.V1.Book;
using TranslationService.Domain.Book.V1.List;

namespace TranslationService.Application.Book.V1
{
    public class BookPostHandler : BookHandler, IRequestHandler<BookRequestPost, BookDTO>
    {
        public BookPostHandler(IRepository<BookDTO, BookFilter> repository)
            : base(repository)
        {
            ;
        }

        public async Task<BookDTO> Handle(BookRequestPost request, CancellationToken cancellationToken)
        {
            if(request.FileType != "text/plain")
            {
                throw new Exception("The type of the file is not correct.");
            }

            using(var streamReader = new StreamReader(request.Stream))
            {
                var guid = await _repository.CreateAsync(new BookDTO { Content = streamReader.ReadToEnd(), Title = request.Title });

                await _repository.SaveAsync();

                return await _repository.GetAsync(guid);
            }
        }
    }
}
