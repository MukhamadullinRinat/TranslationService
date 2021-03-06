using MediatR;

namespace TranslationService.Domain.User.V1.POST
{
    public class UserPostRequest : IRequest<User>
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
