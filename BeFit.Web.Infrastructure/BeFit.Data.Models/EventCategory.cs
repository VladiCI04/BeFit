using System.ComponentModel.DataAnnotations;
using static BeFit.Common.EntityValidationConstants.EventCategory; 

namespace BeFit.Data.Models
{
    public class EventCategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(EventCategoryNameMinLength)]
        [MaxLength(EventCategoryNameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
