using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TranslationService.Domain.Book.V1;
using TranslationService.Domain.Book.V1.DELETE;
using TranslationService.Domain.Book.V1.File;
using TranslationService.Domain.Book.V1.GET;
using TranslationService.Domain.Book.V1.List;
using TranslationService.Domain.Book.V1.POST;
using TranslationService.Domain.Book.V1.PUT;

namespace TranslationService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("book")]
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

            return _mediator.Send(new BookRequestPost {
                Stream = file.OpenReadStream(),
                Title = file.FileName,
                FileType = file.ContentType
            });
        }

        [HttpGet]
        public async Task<Book> GetAsync(Guid guid) =>
            await _mediator.Send(new BookRequestGet { Guid = guid });

        [HttpGet("list")]
        public async Task<IEnumerable<Book>> GetListAsync([FromQuery] BookFilter filter) =>
            await _mediator.Send(filter);

        [HttpPut]
        public async Task<Book> PutAsync(BookRequestPut request) =>
            await _mediator.Send(request);

        [HttpDelete("{guid}")]
        public async Task<Guid> DeleteAsync([FromRoute]Guid guid) =>
            await _mediator.Send(new BookRequestDelete { Guid = guid });

        [AllowAnonymous]
        [HttpGet("file/{guid}")]
        public async Task<IActionResult> GetFileAsync([FromRoute]Guid guid)
        {
            var file = await _mediator.Send(new BookRequestFileGet { Guid = guid });

            return File(file.File, file.ContentType, file.Name);
        }
    }
}