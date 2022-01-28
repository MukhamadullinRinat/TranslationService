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
            if(request.FileType != ".pdf")
            {
                throw new Exception("The type of the file is not correct.");
            }

            var pdfDocument = new PdfDocument(new PdfReader(request.Stream));
            var strategy = new LocationTextExtractionStrategy();
            var processed = new StringBuilder();

            for (int i = 1; i <= pdfDocument.GetNumberOfPages(); ++i)
            {
                var page = pdfDocument.GetPage(i);
                string text = PdfTextExtractor.GetTextFromPage(page, strategy);
                processed.Append(text);
            }
            pdfDocument.Close();

            var guid = await _repository.CreateAsync(new BookDTO { Content = processed.ToString(), Title = request.Title });

            await _repository.SaveAsync();

            return await _repository.GetAsync(guid);
        }
    }
}
