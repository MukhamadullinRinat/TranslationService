using MediatR;

namespace TranslationService.Domain.Book.V1.POST
{
    /// <summary>
    /// Book 
    /// </summary>
    public class BookRequest : IRequest<Book>
    {
        /// <summary>
        /// The title of the book.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Content
        /// </summary>
        public Stream Stream { get; set; }

        /// <summary>
        /// The type of the file.
        /// </summary>
        public string FileType { get; set; }
    }
}
