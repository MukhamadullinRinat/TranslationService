using MediatR;
using TranslationService.Domain;
using TranslationService.Domain.User.V1.GET;
using TranslationService.Domain.User.V1.List;

namespace TranslationService.Application.User.V1
{
    using TranslationService.Domain.User;

    public class UserGetHandler : UserHandler, IRequestHandler<UserGetRequest, User>
    {
        public UserGetHandler(IRepository<User, User, UserFilter> repository)
            : base(repository)
        {
            ;
        }

        public Task<User> Handle(UserGetRequest request, CancellationToken cancellationToken) =>
            _repository.GetAsync(request.Guid);
    }
}
