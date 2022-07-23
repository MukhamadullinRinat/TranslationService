using Newtonsoft.Json;
using System.Text;
using TranslationService.Domain;
using TranslationService.Domain.Book;
using TranslationService.Domain.Book.V1;
using TranslationService.Domain.Book.V1.List;
using TranslationService.Domain.User;

namespace TranslationService.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ICurrentUserService _currentUserService;
        readonly private string _dataPath = Path.Combine($"{Directory.GetCurrentDirectory()}.Infrastructure", "Content/bookData.json");

        readonly private int _pageSize = 50;

        public BookRepository(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
        }

        public async Task<Guid> CreateAsync(BookCreateModel entity)
        {
            var books = (await GetAllBooksAsync((await _currentUserService.GetCurrentUserAsync()).Guid)).ToList();

            var guid = Guid.NewGuid();

            var book = new BookJson {
                Title = entity.Title,
                Guid = guid,
                PageNumber = 1,
                Extension = entity.Extension,
                ContentType = entity.ContentType,
                UserId = (await _currentUserService.GetCurrentUserAsync()).Guid
            };

            books.Add(book);

            var pathCreatedFile = GetContentBookPath(guid, entity.Extension);

            using (var stream = File.Create(pathCreatedFile))
            {
                if (entity.Extension == ".txt")
                {
                    using (var writer = new StreamWriter(stream))
                    {
                        var lines = entity.Content.Split('\n');
                        var builder = new StringBuilder();

                        for (var i = 0; i < lines.Length; i++)
                        {
                            if (i % _pageSize == 0)
                            {
                                var pageNumber = (i / _pageSize) + 1;
                                builder.Append($"\n{{{pageNumber}}}\n");

                                book.PageCount = pageNumber;
                            }

                            builder.Append(lines[i]);
                        }

                        await writer.WriteAsync(builder.ToString());
                    }
                }
                else
                {
                    entity.Stream.Seek(0, SeekOrigin.Begin);
                    await entity.Stream.CopyToAsync(stream);
                }
            }

            await File.WriteAllTextAsync(_dataPath, JsonConvert.SerializeObject(books));

            return guid;
        }

        public async Task<Guid> DeleteAsync(Guid guid)
        {
            var books = await GetAllBooksAsync((await _currentUserService.GetCurrentUserAsync()).Guid);
            var bookJson = books.FirstOrDefault(b => b.Guid == guid);

            await File.WriteAllTextAsync(_dataPath, JsonConvert.SerializeObject(books.Where(b => !b.Guid.Equals(guid))));

            File.Delete(GetContentBookPath(guid, bookJson.Extension));

            return guid;
        }

        public void Dispose()
        {
            ;
        }

        public async Task<IEnumerable<Book>> GetAllAsync(BookFilter filter)
        {
            var books = (await GetAllBooksAsync((await _currentUserService.GetCurrentUserAsync()).Guid)).Select(b => new Book
            {
                Guid = b.Guid,
                PageCount = b.PageCount,
                PageNumber = b.PageNumber,
                Title = b.Title,
                Extension = b.Extension,
                ContentType = b.ContentType
            });

            if (!string.IsNullOrEmpty(filter.Title))
            {
                books = books.Where(book => book.Title == filter.Title);
            }

            return books;
        }

        public async Task<Book> GetAsync(Guid id)
        {
            var books = await GetAllBooksAsync();
            var bookJson = books.FirstOrDefault(b => b.Guid == id);
            var content = string.Empty;

            if (bookJson.Extension == ".txt")
            {
                var bookText = await File.ReadAllTextAsync(GetContentBookPath(id, bookJson.Extension));
                var pageNumberPosition = bookText.IndexOf($"{{{bookJson.PageNumber}}}") + 4;
                var nextPageNumberPosition = bookText.IndexOf($"{{{bookJson.PageNumber + 1}}}");
                var length = nextPageNumberPosition == -1 ? bookText.Length - pageNumberPosition : nextPageNumberPosition - pageNumberPosition;
                content = bookText.Substring(pageNumberPosition, length);
            }

            return new Book
            {
                Guid = bookJson.Guid,
                Title = bookJson.Title,
                PageNumber = bookJson.PageNumber,
                Content = content,
                PageCount = bookJson.PageCount,
                Extension = bookJson.Extension,
                ContentType = bookJson.ContentType
            };
        }

        public async Task<FileStream> GetStreamAsync(Guid guid)
        {
            var books = await GetAllBooksAsync();
            var bookJson = books.FirstOrDefault(b => b.Guid == guid);

            return File.OpenRead(GetContentBookPath(guid, bookJson.Extension));
        }

        public Task SaveAsync()
        {
            return Task.CompletedTask;
        }

        public async Task UpdateAsync(Book entity)
        {
            var books = (await GetAllBooksAsync((await _currentUserService.GetCurrentUserAsync()).Guid)).Select(b => {
                if (entity.Guid.Equals(b.Guid))
                {
                    b.Title = entity.Title;
                    b.PageNumber = entity.PageNumber;
                }

                return b;
            });

            await File.WriteAllTextAsync(_dataPath, JsonConvert.SerializeObject(books));
        }

        private async Task<IEnumerable<BookJson>> GetAllBooksAsync(Guid? userId = null)
        {
            var jsonData = await File.ReadAllTextAsync(_dataPath);

            var books = JsonConvert.DeserializeObject<List<BookJson>>(jsonData);

            if (books == null)
            {
                throw new Exception($"{nameof(books)} is null");
            }

            return userId.HasValue ? books.Where(book => book.UserId.Equals(userId.Value)) : books;
        }

        private string GetContentBookPath(Guid guid, string extension) =>
            Path.Combine($"{Directory.GetCurrentDirectory()}.Infrastructure", $"Content/Books/${guid}{extension}");
    }
}
