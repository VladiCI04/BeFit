using BeFit.Web.ViewModels.Category;
using System.ComponentModel.DataAnnotations;
using static BeFit.Common.EntityValidationConstants.Event;

namespace BeFit.Web.ViewModels.Event
{
    public class EventFormModel
    {
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
        public string CoachName { get; set; } = null!;

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

        public int EventCategoryId { get; set; }
        public IEnumerable<EventSelectCategoryFormModel> EventsCategories { get; set; } = new List<EventSelectCategoryFormModel>();
    }
}
