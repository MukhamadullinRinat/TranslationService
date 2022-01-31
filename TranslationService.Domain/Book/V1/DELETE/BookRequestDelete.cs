using MediatR;

namespace TranslationService.Domain.Book.V1.DELETE
{
    public class BookRequestDelete : IRequest<Guid>
    {
        public Guid Guid { get; set; }
    }
}
