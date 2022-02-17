using TranslationService.Domain;
using TranslationService.Domain.User.V1.List;

namespace TranslationService.Application.User.V1
{
    using TranslationService.Domain.User;
    public abstract class UserHandler
    {
        protected readonly IRepository<User, User, UserFilter> _repository;

        public UserHandler(IRepository<User, User, UserFilter> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
    }
}
