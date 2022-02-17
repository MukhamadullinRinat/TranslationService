using MediatR;
using TranslationService.Domain;
using TranslationService.Domain.Word.V1.GET;
using TranslationService.Domain.Word.V1.List;

namespace TranslationService.Application.Word.V1
{
    using TranslationService.Domain.Word.V1;
    public class WordGetHandler : WordHandler, IRequestHandler<WordRequestGet, Word>
    {
        public WordGetHandler(IRepository<Word, Word, WordFilter> repository)
            : base(repository)
        {
            ;
        }

        public Task<Word> Handle(WordRequestGet request, CancellationToken cancellationToken) =>
            _repository.GetAsync(request.Guid);
    }
}
