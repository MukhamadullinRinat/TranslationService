using MediatR;

namespace TranslationService.Domain.User.V1.DELETE
{
    public class UserDeleteRequest : IRequest<Guid>
    {
        public Guid Guid { get; set; }
    }
}
