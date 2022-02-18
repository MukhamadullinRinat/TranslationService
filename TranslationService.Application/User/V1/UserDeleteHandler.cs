using MediatR;
using TranslationService.Domain;
using TranslationService.Domain.User.V1.DELETE;
using TranslationService.Domain.User.V1.List;

namespace TranslationService.Application.User.V1
{
    using TranslationService.Domain.User;
    public class UserDeleteHandler : UserHandler, IRequestHandler<UserDeleteRequest, Guid>
    {
        public UserDeleteHandler(IRepository<User, User, UserFilter> repository)
            : base(repository)
        {
            ;
        }
        public Task<Guid> Handle(UserDeleteRequest request, CancellationToken cancellationToken) =>
            _repository.DeleteAsync(request.Guid);
    }
}
