using MediatR;

namespace TranslationService.Domain.User.V1.PUT
{
    public class UserPutRequest : IRequest<User>
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
