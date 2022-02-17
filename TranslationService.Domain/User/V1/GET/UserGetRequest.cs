using MediatR;

namespace TranslationService.Domain.User.V1.GET
{
    public class UserGetRequest : IRequest<User>
    {
        public Guid Guid { get; set; }
    }
}
