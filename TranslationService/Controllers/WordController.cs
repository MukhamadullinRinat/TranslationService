using MediatR;
using Microsoft.AspNetCore.Mvc;
using TranslationService.Domain.Word.V1;
using TranslationService.Domain.Word.V1.DELETE;
using TranslationService.Domain.Word.V1.GET;
using TranslationService.Domain.Word.V1.List;
using TranslationService.Domain.Word.V1.POST;
using TranslationService.Domain.Word.V1.PUT;

namespace TranslationService.Controllers
{
    [ApiController]
    [Route("word")]
    public class WordController : ControllerBase
    {
        private readonly ILogger<WordController> _logger;
        private readonly IMediator _mediator;


        public WordController(ILogger<WordController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public Task<Word> PostAsync(WordRequestPost request) =>
            _mediator.Send(request);

        [HttpGet]
        public async Task<Word> GetAsync(Guid guid) =>
            await _mediator.Send(new WordRequestGet { Guid = guid });

        [HttpGet("list")]
        public async Task<IEnumerable<Word>> GetListAsync([FromQuery] WordFilter filter) =>
            await _mediator.Send(filter);

        [HttpPut]
        public async Task<Word> PutAsync(WordRequestPut request) =>
            await _mediator.Send(request);

        [HttpDelete("{guid}")]
        public async Task<Guid> DeleteAsync([FromRoute] Guid guid) =>
            await _mediator.Send(new WordRequestDelete { Guid = guid });
    }
}
