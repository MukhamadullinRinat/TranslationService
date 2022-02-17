using MediatR;
using TranslationService.Domain;
using TranslationService.Domain.User.V1.List;
using TranslationService.Domain.User.V1.PUT;
using System.Threading;
using System.Threading.Tasks;

namespace TranslationService.Application.User.V1
{
    using TranslationService.Domain.User;
    public class UserPutHandler : UserHandler, IRequestHandler<UserPutRequest, User>
    {
        public UserPutHandler(IRepository<User, User, UserFilter> repository)
            : base(repository)
        {
            ;
        }

        public async Task<User> Handle(UserPutRequest request, CancellationToken cancellationToken)
        {
            await _repository.UpdateAsync(new User { Email = request.Email, Password = request.Password, Name = request.Name, Guid = request.Guid });

            return await _repository.GetAsync(request.Guid);
        }
    }
}
