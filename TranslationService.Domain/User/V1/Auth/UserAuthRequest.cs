using MediatR;

namespace TranslationService.Domain.User.V1.Auth
{
    using TranslationService.Domain.User;

    public class UserAuthRequest : IRequest<User>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
