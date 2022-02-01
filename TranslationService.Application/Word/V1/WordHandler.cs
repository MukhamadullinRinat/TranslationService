using TranslationService.Domain;
using TranslationService.Domain.Word.V1.List;
using WordDTO = TranslationService.Domain.Word.V1.Word;

namespace TranslationService.Application.Word.V1
{
    public class WordHandler
    {
        protected readonly IRepository<WordDTO, WordFilter> _repository;

        public WordHandler(IRepository<WordDTO, WordFilter> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
    }
}
