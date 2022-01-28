using MediatR;
using Microsoft.AspNetCore.Mvc;
using TranslationService.Domain.Book.V1;
using TranslationService.Domain.Book.V1.POST;

namespace TranslationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IMediator _mediator;


        public BookController(ILogger<BookController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public Task<Book> PostAsync(List<IFormFile> files)
        {
            var file = files.First();

            return _mediator.Send(new BookRequest {
                Stream = file.OpenReadStream(),
                Title = file.FileName,
                FileType = file.ContentType
            });
        }
    }
}