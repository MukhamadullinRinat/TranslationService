using MediatR;
using TranslationService.Domain.Book.V1.POST;
using TranslationService.Domain;
using BookDTO = TranslationService.Domain.Book.V1.Book;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using System.Text;
using iText.Kernel.Pdf.Canvas.Parser;

namespace TranslationService.Application.Book.V1
{
    public class BookPostHandler : IRequestHandler<BookRequest, BookDTO>
    {
        private readonly IRepository<BookDTO> _repository;

        public BookPostHandler(IRepository<BookDTO> repository)
        {
            _repository = repository;
        }

        public async Task<BookDTO> Handle(BookRequest request, CancellationToken cancellationToken)
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
