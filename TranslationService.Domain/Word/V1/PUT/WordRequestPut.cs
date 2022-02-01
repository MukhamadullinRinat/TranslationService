using MediatR;
using System.ComponentModel.DataAnnotations;

namespace TranslationService.Domain.Word.V1.PUT
{
    public class WordRequestPut : IRequest<Word>
    {
        public string Description { get; set; }

        public DateTime DateToRepeate { get; set; }

        public int RepetitionNumber { get; set; }

        [Required]
        public Guid Guid { get; set; }
    }
}
