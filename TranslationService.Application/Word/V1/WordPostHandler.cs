using MediatR;
using TranslationService.Domain;
using TranslationService.Domain.Word.V1.List;
using TranslationService.Domain.Word.V1.POST;

namespace TranslationService.Application.Word.V1
{
    using TranslationService.Domain.User;
    using TranslationService.Domain.Word.V1;

    public class WordPostHandler : WordHandler, IRequestHandler<WordRequestPost, Word>
    {
        private readonly ICurrentUserService _currentUserService;

        public WordPostHandler(
            IRepository<Word, Word, WordFilter> repository,
            ICurrentUserService currentUserService)
            : base(repository)
        {
            _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
        }

        public async Task<Word> Handle(WordRequestPost request, CancellationToken cancellationToken)
        {
            var user = await _currentUserService.GetCurrentUserAsync();
            var word = new Word {
                Description = request.Description,
                Value = request.Value,
                DateToRepeate = request.DateToRepeate,
                UserId = user.Guid
            };

            var guid = await _repository.CreateAsync(word);

            return await _repository.GetAsync(guid);
        }
    }
}
