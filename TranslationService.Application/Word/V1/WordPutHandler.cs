using MediatR;
using TranslationService.Domain;
using TranslationService.Domain.Word.V1.List;
using TranslationService.Domain.Word.V1.PUT;
using WordDTO = TranslationService.Domain.Word.V1.Word;

namespace TranslationService.Application.Word.V1
{
    public class WordPutHandler : WordHandler, IRequestHandler<WordRequestPut, WordDTO>
    {
        public WordPutHandler(IRepository<WordDTO, WordDTO, WordFilter> repository)
            : base(repository)
        {
            ;
        }

        public async Task<WordDTO> Handle(WordRequestPut request, CancellationToken cancellationToken)
        {
            await _repository.UpdateAsync(new WordDTO {
                DateToRepeate = request.DateToRepeate,
                RepetitionNumber = request.RepetitionNumber,
                Description = request.Description,
                Guid = request.Guid
            });

            return await _repository.GetAsync(request.Guid);
        }
    }
}
