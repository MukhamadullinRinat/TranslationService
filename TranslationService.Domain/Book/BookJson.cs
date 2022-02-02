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

        /// <summary>
        /// Active page number.
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// The overall number of the pages in the book.
        /// </summary>
        public int PageCount { get; set; }

        public string Extention { get; set; }
    }
}
