using TranslationService.Domain;
using TranslationService.Domain.Word.V1.List;

namespace TranslationService.Application.Word.V1
{
    using TranslationService.Domain.Word.V1;

    public abstract class WordHandler
    {
        protected readonly IRepository<Word, Word, WordFilter> _repository;

        public WordHandler(IRepository<Word, Word, WordFilter> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
    }
}
