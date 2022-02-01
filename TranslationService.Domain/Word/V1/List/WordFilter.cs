using MediatR;
using System.ComponentModel;

namespace TranslationService.Domain.Word.V1.List
{
    public class WordFilter : IRequest<IEnumerable<Word>>
    {
        [DefaultValue(null)]
        public string? Value { get; set; }
    }
}
