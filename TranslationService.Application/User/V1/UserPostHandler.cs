using MediatR;
using TranslationService.Domain;
using TranslationService.Domain.User.V1.List;
using TranslationService.Domain.User.V1.POST;
using System.Threading;
using System.Threading.Tasks;

namespace TranslationService.Application.User.V1
{
    using TranslationService.Domain.User;

    public class UserPostHandler : UserHandler, IRequestHandler<UserPostRequest, User>
    {
        public UserPostHandler(IRepository<User, User, UserFilter> repository)
            : base(repository)
        {
            ;
        }

        public async Task<User> Handle(UserPostRequest request, CancellationToken cancellationToken)
        {
            var guid = await _repository.CreateAsync(new User { Email = request.Email, Name = request.Name, Password = request.Password });

            return await _repository.GetAsync(guid);
        }
    }
}
