using Newtonsoft.Json;
using System.Text;
using TranslationService.Domain;
using TranslationService.Domain.Book;
using TranslationService.Domain.Book.V1;
using TranslationService.Domain.Book.V1.List;

namespace TranslationService.Infrastructure.Repositories
{
    public class BookRepository : IRepository<Book, BookFilter>
    {
        readonly private string _dataPath = Path.Combine($"{Directory.GetCurrentDirectory()}.Infrastructure", "Content/bookData.json");

        readonly private int _pageSize = 50;

        public async Task<Guid> CreateAsync(Book entity)
        {
            var books = await GetAllBooksAsync();

            var guid = Guid.NewGuid();

            var book = new BookJson { Title = entity.Title, Guid = guid, PageNumber = 1 };

            books.Add(book);

            var pathCreatedFile = GetContentBookPath(guid);

            using (var stream = File.Create(pathCreatedFile))
                using(var writer = new StreamWriter(stream))
            {
                var lines = entity.Content.Split('\n');
                var builder = new StringBuilder();

                for(var i = 0; i < lines.Length; i++)
                {
                    if(i % _pageSize == 0)
                    {
                        var pageNumber = (i / _pageSize) + 1;
                        builder.Append($"\n{{{pageNumber}}}\n");

                        book.PageCount = pageNumber;
                    }

                    builder.Append(lines[i]);
                }

                await writer.WriteAsync(builder.ToString());
            }

            await File.WriteAllTextAsync(_dataPath, JsonConvert.SerializeObject(books));

            return guid;
        }

        public async Task<Guid> DeleteAsync(Guid guid)
        {
            var books = await GetAllBooksAsync();

            await File.WriteAllTextAsync(_dataPath, JsonConvert.SerializeObject(books.Where(b => !b.Guid.Equals(guid))));

            File.Delete(GetContentBookPath(guid));

            return guid;
        }

        public void Dispose()
        {
            ;
        }

        public async Task<IEnumerable<Book>> GetAllAsync(BookFilter filter)
        {
            var books = (await GetAllBooksAsync()).Select(b => new Book { Guid = b.Guid, PageCount = b.PageCount, PageNumber = b.PageNumber, Title = b.Title });

            return filter?.Title != null ? books.Where(b => b.Title == filter.Title) : books;
        }

        public async Task<Book> GetAsync(Guid id)
        {
            var books = await GetAllBooksAsync();
            var bookText = await File.ReadAllTextAsync(GetContentBookPath(id));
            var bookJson = books.FirstOrDefault(b => b.Guid == id);
            var pageNumberPosition = bookText.IndexOf($"{{{bookJson.PageNumber}}}") + 4;
            var nextPageNumberPosition = bookText.IndexOf($"{{{bookJson.PageNumber + 1}}}");
            var length = nextPageNumberPosition == -1 ? bookText.Length - pageNumberPosition : nextPageNumberPosition - pageNumberPosition;
            var pageText = bookText.Substring(pageNumberPosition, length);

            return new Book
            {
                Guid = bookJson.Guid,
                Title = bookJson.Title,
                PageNumber = bookJson.PageNumber,
                Content = pageText,
                PageCount = bookJson.PageCount
            };
        }

        public Task SaveAsync()
        {
            return Task.CompletedTask;
        }

        public async Task UpdateAsync(Book entity)
        {
            var books = (await GetAllBooksAsync()).Select(b => {
                if (entity.Guid.Equals(b.Guid))
                {
                    b.Title = entity.Title;
                    b.PageNumber = entity.PageNumber;
                }

                return b;
            });

            await File.WriteAllTextAsync(_dataPath, JsonConvert.SerializeObject(books));
        }

        private async Task<List<BookJson>> GetAllBooksAsync()
        {
            var jsonData = await File.ReadAllTextAsync(_dataPath);

            var books = JsonConvert.DeserializeObject<List<BookJson>>(jsonData);

            if (books == null)
            {
                throw new Exception($"{nameof(books)} is null");
            }

            return books;
        }

        private string GetContentBookPath(Guid guid) =>
            Path.Combine($"{Directory.GetCurrentDirectory()}.Infrastructure", $"Content/Books/${guid}.txt");
    }
}
