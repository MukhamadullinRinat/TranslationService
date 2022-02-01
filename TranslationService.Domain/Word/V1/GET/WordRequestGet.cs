using MediatR;

namespace TranslationService.Domain.Word.V1.GET
{
    public class WordRequestGet : IRequest<Word>
    {
        public Guid Guid { get; set; }
    }
}
