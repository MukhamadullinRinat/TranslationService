using MediatR;
using TranslationService.Domain;
using TranslationService.Domain.Word.V1.DELETE;
using TranslationService.Domain.Word.V1.List;
using WordDTO = TranslationService.Domain.Word.V1.Word;

namespace TranslationService.Application.Word.V1
{
    public class WordDeleteHandler : WordHandler, IRequestHandler<WordRequestDelete, Guid>
    {
        public WordDeleteHandler(IRepository<WordDTO, WordFilter> repository)
            : base(repository)
        {
            ;
        }

        public Task<Guid> Handle(WordRequestDelete request, CancellationToken cancellationToken) =>
            _repository.DeleteAsync(request.Guid);
    }
}
