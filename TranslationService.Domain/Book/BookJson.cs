namespace TranslationService.Domain.Book
{
    public class BookJson
    {
        /// <summary>
        /// The title of the book.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Identificator.
        /// </summary>
        public Guid Guid { get; set; }
    }
}
