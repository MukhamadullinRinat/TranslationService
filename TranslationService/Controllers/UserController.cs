using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TranslationService.Domain.User.V1.DELETE;
using TranslationService.Domain.User.V1.GET;
using TranslationService.Domain.User.V1.List;
using TranslationService.Domain.User.V1.POST;
using TranslationService.Domain.User.V1.PUT;

namespace TranslationService.Controllers
{
    using TranslationService.Domain.User;
    using TranslationService.Domain.User.V1.Auth;

    [Authorize]
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost]
        public Task<User> PostAsync(UserPostRequest request) =>
            _mediator.Send(request);

        [HttpGet]
        public Task<User> GetAsync(Guid guid) =>
            _mediator.Send(new UserGetRequest { Guid = guid });

        [HttpPut]
        public async Task<User> PutAsync(UserPutRequest request) =>
            await _mediator.Send(request);

        [HttpGet("list")]
        public async Task<IEnumerable<User>> GetListAsync([FromQuery] UserFilter filter) =>
            await _mediator.Send(filter);

        [HttpDelete("{guid}")]
        public async Task<Guid> DeleteAsync([FromRoute] Guid guid) =>
            await _mediator.Send(new UserDeleteRequest { Guid = guid });

        [AllowAnonymous]
        [HttpPost("auth")]
        public async Task<User> AuthAsync(UserAuthRequest request) =>
            await _mediator.Send(request);
    }
}
