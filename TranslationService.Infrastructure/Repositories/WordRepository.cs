using Newtonsoft.Json;
using TranslationService.Domain;
using TranslationService.Domain.Word.V1;
using TranslationService.Domain.Word.V1.List;

namespace TranslationService.Infrastructure.Repositories
{
    public class WordRepository : IRepository<Word, Word, WordFilter>
    {
        readonly private string _dataPath = Path.Combine($"{Directory.GetCurrentDirectory()}.Infrastructure", "Content/wordData.json");

        public async Task<Guid> CreateAsync(Word entity)
        {
            var words = (await GetAllWordsAsync()).ToList();

            entity.Guid = Guid.NewGuid();
            entity.Value = entity.Value.Trim().ToLower();

            words.Add(entity);

            await File.WriteAllTextAsync(_dataPath, JsonConvert.SerializeObject(words));

            return entity.Guid;
        }

        public async Task<Guid> DeleteAsync(Guid guid)
        {
            var words = await GetAllWordsAsync();
            var word = words.FirstOrDefault(w => w.Guid == guid);

            if (word == null)
            {
                throw new Exception("No word with this identificator");
            }

            word.Closed = DateTime.Now;

            await File.WriteAllTextAsync(_dataPath, JsonConvert.SerializeObject(words));

            return guid;
        }

        public async Task<IEnumerable<Word>> GetAllAsync(WordFilter filter)
        {
            var words = await GetAllWordsAsync();

            if (!string.IsNullOrEmpty(filter.Value))
            {
                words = words.Where(word => word.Value == filter.Value.ToLower().Trim());
            }

            if (filter.UserId.HasValue)
            {
                words = words.Where(word => word.UserId.Equals(filter.UserId.Value));
            }

            if (filter.Actual)
            {
                words = words.Where(word => word.Closed == null);
            }

            return words;
        }

        public async Task<Word> GetAsync(Guid id) =>
            (await GetAllWordsAsync()).FirstOrDefault(word => word.Guid == id) ?? throw new NullReferenceException(nameof(GetAsync));

        public async Task UpdateAsync(Word entity)
        {
            var words = await GetAllWordsAsync();
            var word = words.FirstOrDefault(w => w.Guid == entity.Guid) ?? throw new NullReferenceException(nameof(entity));

            word.DateToRepeate = entity.DateToRepeate;
            word.Description = entity.Description;
            word.RepetitionNumber = entity.RepetitionNumber;

            await File.WriteAllTextAsync(_dataPath, JsonConvert.SerializeObject(words));
        }

        public void Dispose()
        {
            ;
        }

        public Task SaveAsync()
        {
            return Task.CompletedTask;
        }

        private async Task<IEnumerable<Word>> GetAllWordsAsync()
        {
            var jsonData = await File.ReadAllTextAsync(_dataPath);

            var books = JsonConvert.DeserializeObject<IEnumerable<Word>>(jsonData);

            if (books == null)
            {
                throw new Exception($"{nameof(books)} is null");
            }

            return books;
        }
    }
}
