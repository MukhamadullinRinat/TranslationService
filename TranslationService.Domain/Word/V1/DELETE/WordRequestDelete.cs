using MediatR;

namespace TranslationService.Domain.Word.V1.DELETE
{
    public class WordRequestDelete : IRequest<Guid>
    {
        public Guid Guid { get; set; }
    }
}
