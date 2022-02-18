namespace TranslationService.Domain.User.V1.Auth
{
    public interface IUserAuthService
    {
        Task<User> Authenticate(string email, string password);
    }
}
