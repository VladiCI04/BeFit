using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BeFit.Data.Models
{
    public class EventClient
    {
        [Required]
        [ForeignKey(nameof(Client))]
        public Guid ClientId { get; set; }
        public ApplicationUser Client { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Event))]
        public Guid EventId { get; set; }
        public Event Event { get; set; } = null!;
    }
}
