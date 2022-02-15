using TranslationService.Domain.User.V1.Auth;

namespace TranslationService.Application.User.V1.Auth
{
    public class UserAuthService : IUserAuthService
    {
        public Task<Domain.User.User> Authenticate(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
