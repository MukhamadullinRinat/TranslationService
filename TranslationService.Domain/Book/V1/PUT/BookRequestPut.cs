using MediatR;

namespace TranslationService.Domain.Book.V1.PUT
{
    public class BookRequestPut : IRequest<Book>
    {
        /// <summary>
        /// The title of the book.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Identificator.
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// Active Page number.
        /// </summary>
        public int? PageNumber { get; set; }
    }
}
