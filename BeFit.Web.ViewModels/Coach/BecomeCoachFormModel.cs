using BeFit.Web.ViewModels.CoachCategory;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static BeFit.Common.EntityValidationConstants.Coach;

namespace BeFit.Web.ViewModels.Coach
{
	public class BecomeCoachFormModel
	{
		[Required]
		[MinLength(CoachNameMinLength)]
		[MaxLength(CoachNameMaxLength)]
		public string Name { get; set; } = null!;

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
		[Phone]
		[Display(Name = "Phone")]
		public string PhoneNumber { get; set; } = null!;

		[Required]
		[MinLength(CoachEmailMinLength)]
		[MaxLength(CoachEmailMaxLength)]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; } = null!;

		[MaxLength(CoachDescriptionMaxLength)]
		[Description]
		[Display(Name = "Description")]
		public string? Description { get; set; }

		[Display(Name = "Category")]
		public int CoachCategoryId { get; set; }
		public IEnumerable<CoachSelectCategoryFormModel> CoachCategories { get; set; } = new List<CoachSelectCategoryFormModel>();
	}
}
