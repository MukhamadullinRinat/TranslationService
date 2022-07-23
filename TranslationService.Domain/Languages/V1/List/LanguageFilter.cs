using MediatR;
using System.ComponentModel;

namespace TranslationService.Domain.Languages.V1.List
{
    public class LanguageFilter : IRequest<IEnumerable<Language>>
    {
        [DefaultValue(null)]
        public string? FullName { get; set; }
    }
}
