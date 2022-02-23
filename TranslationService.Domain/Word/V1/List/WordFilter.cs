using MediatR;
using System.ComponentModel;

namespace TranslationService.Domain.Word.V1.List
{
    public class WordFilter : IRequest<IEnumerable<Word>>
    {
        [DefaultValue(null)]
        public string? Value { get; set; }

        [DefaultValue(null)]
        public Guid? UserId { get; set; }

        [DefaultValue(true)]
        public bool Actual { get; set; }
    }
}
