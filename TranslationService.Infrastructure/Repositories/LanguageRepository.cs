using Newtonsoft.Json;
using TranslationService.Domain;
using TranslationService.Domain.Languages;
using TranslationService.Domain.Languages.V1.List;

namespace TranslationService.Infrastructure.Repositories
{
    public class LanguageRepository : IRepository<Language, Language, LanguageFilter>
    {
        readonly private string _dataPath = Path.Combine($"{Directory.GetCurrentDirectory()}.Infrastructure", "Content/languageData.json");

        public Task<Guid> CreateAsync(Language entity)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> DeleteAsync(Guid guid)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            return;
        }

        public async Task<IEnumerable<Language>> GetAllAsync(LanguageFilter filter)
        {
            var languages = await GetAllLanguagesAsync();

            if (!string.IsNullOrEmpty(filter.FullName))
            {
                languages = languages.Where(l => l.FullName.ToLower() == filter.FullName.ToLower());
            }

            return languages;
        }

        public Task<Language> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Language entity)
        {
            throw new NotImplementedException();
        }

        private async Task<IEnumerable<Language>> GetAllLanguagesAsync()
        {
            var jsonData = await File.ReadAllTextAsync(_dataPath);

            var languages = JsonConvert.DeserializeObject<IEnumerable<Language>>(jsonData);

            if (languages == null)
            {
                throw new Exception($"{nameof(languages)} is null");
            }

            return languages;
        }
    }
}
