using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Text;
using TranslationService.Domain;
using TranslationService.Domain.User;
using TranslationService.Domain.User.V1.List;

namespace TranslationService.Application.User
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<Domain.User.User, Domain.User.User, UserFilter> _repository;

        public CurrentUserService(
            IHttpContextAccessor httpContextAccessor,
            IRepository<Domain.User.User, Domain.User.User, UserFilter> repository)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Domain.User.User> GetCurrentUserAsync()
        {
            var authHeader = AuthenticationHeaderValue.Parse(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
            var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
            var email = credentials[0];

            var users = await _repository.GetAllAsync(new UserFilter { Email = email });

            if (!users.Any())
            {
                throw new Exception("The user with this email was not found");
            } else if(users.Count() > 1)
            {
                throw new Exception("More than one user was found with this email");
            }

            var user = users.Single();

            return user;
        }
    }
}
