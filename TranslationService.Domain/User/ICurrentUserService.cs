namespace TranslationService.Domain.User
{
    public interface ICurrentUserService
    {
        Task<User> GetCurrentUserAsync();
    }
}
