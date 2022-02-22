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
            var user = (await _repository.GetAllAsync(new UserFilter { Email = request.Email, Password = request.Password })).FirstOrDefault();

            if (user != null)
            {
                throw new Exception("The user with this email and this password already existes. Please change the password!");
            }

            var guid = await _repository.CreateAsync(new User { Email = request.Email, Name = request.Name, Password = request.Password });

            return await _repository.GetAsync(guid);
        }
    }
}
