using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static BeFit.Common.EntityValidationConstants.Event;

namespace BeFit.Data.Models
{
    public class Event
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MinLength(EventTitleMinLength)]
        [MaxLength(EventTitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MinLength(EventDescriptionMinLength)]
        [MaxLength(EventDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        [MinLength(EventAddressMinLength)]
        [MaxLength(EventAddressMaxLength)]
        public string Address { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [Range(EventTaxMin, EventTaxMax)]
        public double Tax { get; set; }

        [Required]
        [ForeignKey(nameof(Coach))]
        public Guid CoachId { get; set; }
        public Coach Coach { get; set; } = null!;

        [Required]
        public DateTime CreatedOn { get; set; }

        public bool IsActive { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

        [Required]
        [ForeignKey(nameof(EventCategory))]
        public int EventCategoryId { get; set; }
        public EventCategory EventCategory { get; set; } = null!;

        public ICollection<EventClient> EventClients { get; set; } = new List<EventClient>();

        public ICollection<ApplicationUser> Clients { get; set; } = new List<ApplicationUser>();
    }
}
