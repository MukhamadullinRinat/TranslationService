using MediatR;
using TranslationService.Domain;
using TranslationService.Domain.User.V1.List;

namespace TranslationService.Application.User.V1
{
    using TranslationService.Domain.User;
    public class UserListHandler : UserHandler, IRequestHandler<UserFilter, IEnumerable<User>>
    {
        public UserListHandler(IRepository<User, User, UserFilter> repository)
            : base(repository)
        {
            ;
        }

        public Task<IEnumerable<User>> Handle(UserFilter request, CancellationToken cancellationToken) =>
            _repository.GetAllAsync(request);
    }
}
