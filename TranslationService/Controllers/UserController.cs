using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TranslationService.Domain.User.V1.POST;

namespace TranslationService.Controllers
{
    using TranslationService.Domain.User;

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

        [HttpPost]
        public Task<User> PostAsync(UserPostRequest request) =>
            _mediator.Send(request);
    }
}
