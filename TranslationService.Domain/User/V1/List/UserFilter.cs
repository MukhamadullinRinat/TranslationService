using MediatR;
using System.ComponentModel;

namespace TranslationService.Domain.User.V1.List
{
    public class UserFilter : IRequest<IEnumerable<User>>
    {
        [DefaultValue(null)]
        public string? Name { get; set; }

        [DefaultValue(null)]
        public string? Email { get; set; }

        [DefaultValue(null)]
        public string? Password { get; set; }
    }
}
