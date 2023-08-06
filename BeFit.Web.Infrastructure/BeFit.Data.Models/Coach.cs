using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static BeFit.Common.EntityValidationConstants.Coach;

namespace BeFit.Data.Models
{
    public class Coach
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Range(CoachAgeMin, CoachAgeMax)]
        public int Age { get; set; }

        [Required]
        public string Gender { get; set; } = null!;

        [Range(CoachHeightMin, CoachHeightMax)]
        public double Height { get; set; }

        [Range(CoachWeightMin, CoachWeightMax)]
        public double Weight { get; set; }

        [Required]
        [MinLength(CoachPhoneNumberMinLength)]
        [MaxLength(CoachPhoneNumberMaxLength)]
        public string PhoneNumber { get; set; } = null!;

        [MaxLength(CoachDescriptionMaxLength)]
        public string? Description { get; set; }

		[Required]
		[ForeignKey(nameof(CoachCategory))]
		public int CoachCategoryId { get; set; }
		public CoachCategory CoachCategory { get; set; } = null!;

		[ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public virtual ApplicationUser User { get; set; } = null!;

        public virtual ICollection<Event> Events { get; set; } = new List<Event>();
    }
}