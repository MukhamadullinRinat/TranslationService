using MediatR;
using TranslationService.Domain.User.V1.Auth;

namespace TranslationService.Application.User.V1.Auth
{
    using TranslationService.Domain.User;

    public class UserAuthHandler : IRequestHandler<UserAuthRequest, User>
    {
        private readonly IUserAuthService _userAuthService;

        public UserAuthHandler(IUserAuthService userAuthService)
        {
            _userAuthService = userAuthService ?? throw new ArgumentNullException(nameof(userAuthService));
        }

        public Task<User> Handle(UserAuthRequest request, CancellationToken cancellationToken) =>
            _userAuthService.Authenticate(request.Email, request.Password);
    }
}
