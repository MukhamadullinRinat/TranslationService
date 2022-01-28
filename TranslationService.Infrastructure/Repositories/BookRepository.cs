using System.Reflection;
using System.Text;
using System.Text.Json;
using TranslationService.Domain;
using TranslationService.Domain.Book;
using TranslationService.Domain.Book.V1;

namespace TranslationService.Infrastructure.Repositories
{
    public class BookRepository : IRepository<Book>
    {
        public async Task<Guid> CreateAsync(Book entity)
        {
            var path = $"{Assembly.GetCallingAssembly().Location}/Content/bookData.json";
            List<BookJson> books = null;

            using (var openStream = File.OpenRead(path))
            {
                if(openStream == null)
                {
                    throw new Exception($"{nameof(openStream)} is null");
                }
                books = await JsonSerializer.Deserialize<Task<List<BookJson>>>(openStream);

                if (books == null)
                {
                    throw new Exception("The book section is not found");
                }
            }

            var guid = new Guid();

            var book = new BookJson { Title = entity.Title, Guid = guid };

            books.Add(book);

            using (var writeStream = File.OpenWrite(path))
            {
                await JsonSerializer.SerializeAsync(writeStream, books);
            }

            await File.WriteAllTextAsync($"{Assembly.GetCallingAssembly().Location}/Content/${guid}.txt", entity.Content);

            return guid;
        }

        public Task DeleteAsync(Book entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            ;
        }

        public Task<Book> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync()
        {
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Book entity)
        {
            throw new NotImplementedException();
        }
    }
}
