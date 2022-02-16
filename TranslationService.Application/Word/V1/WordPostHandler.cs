using MediatR;
using TranslationService.Domain;
using TranslationService.Domain.Word.V1.List;
using TranslationService.Domain.Word.V1.POST;

namespace TranslationService.Application.Word.V1
{
    using TranslationService.Domain.Word.V1;

    public class WordPostHandler : WordHandler, IRequestHandler<WordRequestPost, Word>
    {
        public WordPostHandler(IRepository<Word, Word, WordFilter> repository)
            : base(repository)
        {
            ;
        }

        public async Task<Word> Handle(WordRequestPost request, CancellationToken cancellationToken)
        {
            var word = new Word { Description = request.Description, Value = request.Value, DateToRepeate = request.DateToRepeate };

            var guid = await _repository.CreateAsync(word);

            return await _repository.GetAsync(guid);
        }
    }
}
