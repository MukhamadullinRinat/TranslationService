using Newtonsoft.Json;
using TranslationService.Domain;
using TranslationService.Domain.User;
using TranslationService.Domain.User.V1.List;

namespace TranslationService.Infrastructure.Repositories
{
    public class UserRepository : IRepository<User, User, UserFilter>
    {
        readonly private string _dataPath = Path.Combine($"{Directory.GetCurrentDirectory()}.Infrastructure", "Content/userData.json");

        public async Task<Guid> CreateAsync(User entity)
        {
            var users = (await GetAllUsersAsync()).ToList();

            entity.Guid = Guid.NewGuid();

            users.Add(entity);

            await File.WriteAllTextAsync(_dataPath, JsonConvert.SerializeObject(users));

            return entity.Guid;
        }

        public async Task<Guid> DeleteAsync(Guid guid)
        {
            var users = await GetAllUsersAsync();

            users = users.Any(u => u.Guid == guid) ? users.Where(u => u.Guid != guid) : throw new ArgumentException($"{nameof(guid)} is not found");

            await File.WriteAllTextAsync(_dataPath, JsonConvert.SerializeObject(users));

            return guid;
        }

        public void Dispose()
        {
            ;
        }

        public async Task<IEnumerable<User>> GetAllAsync(UserFilter filter)
        {
            var users = await GetAllUsersAsync();

            if (!string.IsNullOrEmpty(filter.Email))
            {
                users = users.Where(u => u.Email == filter.Email);
            }

            if (!string.IsNullOrEmpty(filter.Name))
            {
                users = users.Where(u => u.Name == filter.Name);
            }

            if (!string.IsNullOrEmpty(filter.Password))
            {
                users = users.Where(u => u.Password == filter.Password);
            }

            return users;
        }

        public async Task<User> GetAsync(Guid id) =>
            (await GetAllUsersAsync()).FirstOrDefault(user => user.Guid == id) ?? throw new NullReferenceException(nameof(GetAsync));

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(User entity)
        {
            var users = await GetAllUsersAsync();
            var user = users.FirstOrDefault(u => u.Guid == entity.Guid) ?? throw new NullReferenceException(nameof(entity));

            user.Email = entity.Email;
            user.Name = entity.Name;
            user.Password = entity.Password;

            await File.WriteAllTextAsync(_dataPath, JsonConvert.SerializeObject(users));
        }

        private async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var jsonData = await File.ReadAllTextAsync(_dataPath);

            var users = JsonConvert.DeserializeObject<IEnumerable<User>>(jsonData);

            if (users == null)
            {
                throw new Exception($"{nameof(users)} is null");
            }

            return users;
        }
    }
}
