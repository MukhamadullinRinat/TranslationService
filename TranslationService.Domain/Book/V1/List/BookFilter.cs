using MediatR;
using System.ComponentModel;

namespace TranslationService.Domain.Book.V1.List
{
    public class BookFilter : IRequest<IEnumerable<Book>>
    {
        [DefaultValue(null)]
        public string? Title { get; set; }
    }
}
