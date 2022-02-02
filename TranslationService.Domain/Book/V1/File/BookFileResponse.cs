namespace TranslationService.Domain.Book.V1.File
{
    public class BookFileResponse
    {
        public FileStream File { get; set; }

        public string ContentType { get; set; }

        public string Name { get; set; }
    }
}
