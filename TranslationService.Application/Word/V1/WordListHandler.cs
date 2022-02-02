using MediatR;
using TranslationService.Domain;
using TranslationService.Domain.Word.V1.List;
using WordDTO = TranslationService.Domain.Word.V1.Word;

namespace TranslationService.Application.Word.V1
{
    public class WordListHandler : WordHandler, IRequestHandler<WordFilter, IEnumerable<WordDTO>>
    {
        public WordListHandler(IRepository<WordDTO, WordDTO, WordFilter> repository)
            : base(repository)
        {
            ;
        }

        public Task<IEnumerable<WordDTO>> Handle(WordFilter request, CancellationToken cancellationToken) =>
            _repository.GetAllAsync(request);
    }
}
