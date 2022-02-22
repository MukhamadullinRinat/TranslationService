using TranslationService.Domain;
using TranslationService.Domain.User.V1.Auth;
using TranslationService.Domain.User.V1.List;

namespace TranslationService.Application.User.V1.Auth
{
    using TranslationService.Domain.User;

    public class UserAuthService : IUserAuthService
    {
        private readonly IRepository<User, User, UserFilter> _repository;

        public UserAuthService(IRepository<User, User, UserFilter> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<User> Authenticate(string email, string password) =>
            (await _repository.GetAllAsync(new UserFilter { Email = email, Password = password })).FirstOrDefault();
    }
}
