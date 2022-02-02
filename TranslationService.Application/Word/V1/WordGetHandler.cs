using MediatR;
using TranslationService.Domain;
using TranslationService.Domain.Word.V1.GET;
using TranslationService.Domain.Word.V1.List;
using WordDTO = TranslationService.Domain.Word.V1.Word;

namespace TranslationService.Application.Word.V1
{
    public class WordGetHandler : WordHandler, IRequestHandler<WordRequestGet, WordDTO>
    {
        public WordGetHandler(IRepository<WordDTO, WordDTO, WordFilter> repository)
            : base(repository)
        {
            ;
        }

        public Task<WordDTO> Handle(WordRequestGet request, CancellationToken cancellationToken) =>
            _repository.GetAsync(request.Guid);
    }
}
