using MediatR;
using TranslationService.Domain;
using TranslationService.Domain.Languages.V1.List;
using LanguageEntity = TranslationService.Domain.Languages.Language;

namespace TranslationService.Application.Language.V1
{
    public class LanguageListHandler : IRequestHandler<LanguageFilter, IEnumerable<LanguageEntity>>
    {
        private readonly IRepository<LanguageEntity, LanguageEntity, LanguageFilter> _languageRepository;

        public LanguageListHandler(IRepository<LanguageEntity, LanguageEntity, LanguageFilter> languageRepository)
        {
            _languageRepository = languageRepository ?? throw new ArgumentNullException(nameof(languageRepository));
        }

        public Task<IEnumerable<LanguageEntity>> Handle(LanguageFilter request, CancellationToken cancellationToken) =>
            _languageRepository.GetAllAsync(request);
    }
}
