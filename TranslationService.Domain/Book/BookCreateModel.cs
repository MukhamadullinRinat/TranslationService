namespace TranslationService.Domain.Book
{
    public class BookCreateModel
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

        public string Content { get; set; }

        public bool IsTextType
        {
            get => this.FileType == "text/plain"; 
        }

        public string Extension
        {
            get => Path.GetExtension(this.Title).ToLower();
        }

        public string ContentType
        {
            get => MimeTypeMap.List.MimeTypeMap.GetMimeType(this.Extension).FirstOrDefault();
        }
    }
}
