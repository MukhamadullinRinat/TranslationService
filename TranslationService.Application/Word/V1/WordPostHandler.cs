using MediatR;
using TranslationService.Domain;
using TranslationService.Domain.Word.V1.List;
using TranslationService.Domain.Word.V1.POST;
using WordDTO = TranslationService.Domain.Word.V1.Word;

namespace TranslationService.Application.Word.V1
{
    public class WordPostHandler : WordHandler, IRequestHandler<WordRequestPost, WordDTO>
    {
        public WordPostHandler(IRepository<WordDTO, WordFilter> repository)
            : base(repository)
        {
            ;
        }

        public async Task<WordDTO> Handle(WordRequestPost request, CancellationToken cancellationToken)
        {
            var word = new WordDTO { Description = request.Description, Value = request.Value };

            word.DateToRepeate = DateTime.Now.AddDays(1);

            var guid = await _repository.CreateAsync(word);

            return await _repository.GetAsync(guid);
        }
    }
}
