namespace TranslationService.Domain.Word.V1
{
    public class Word
    {
        public string Value { get; set; }

        public string Description { get; set; }

        public Guid Guid { get; set; }

        public DateTime DateToRepeate { get; set; }

        public int RepetitionNumber { get; set; }
    }
}
