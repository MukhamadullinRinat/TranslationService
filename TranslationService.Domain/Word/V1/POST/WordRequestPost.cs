using MediatR;

namespace TranslationService.Domain.Word.V1.POST
{
    public class WordRequestPost : IRequest<Word>
    {
        public string Value { get; set; }

        public string Description { get; set; }
    }
}
