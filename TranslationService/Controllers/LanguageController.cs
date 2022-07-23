using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LanguageEntity = TranslationService.Domain.Languages.Language;
using TranslationService.Domain.Languages.V1.List;

namespace TranslationService.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("language")]
    public class LanguageController
    {
        private readonly IMediator _mediator;

        public LanguageController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("list")]
        public async Task<IEnumerable<LanguageEntity>> GetListAsync([FromQuery] LanguageFilter filter) =>
            await _mediator.Send(filter);
    }
}
