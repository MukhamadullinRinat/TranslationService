namespace TranslationService.Domain.Book.V1
{
    /// <summary>
    /// Response in BookController.Get V1.
    /// </summary>
    public class Book
    {
        /// <summary>
        /// The title of the book.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The content of the book.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Identificator.
        /// </summary>
        public Guid Guid { get; set; }
    }
}
