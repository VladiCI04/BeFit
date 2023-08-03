using BeFit.Web.ViewModels.CoachCategory;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static BeFit.Common.EntityValidationConstants.Coach;

namespace BeFit.Web.ViewModels.Coach
{
	public class BecomeCoachFormModel
	{
		[Range(CoachAgeMin, CoachAgeMax)]
		[Display(Name = "Your Age")]
		public int Age { get; set; }

		[Required]
		[Display(Name = "Your Gender")]
		public string Gender { get; set; } = null!;

		[Range(CoachHeightMin, CoachHeightMax)]
		[Display(Name = "Height (in m)")]
		public double Height { get; set; }

		[Range(CoachWeightMin, CoachWeightMax)]
		[Display(Name = "Weight (in kg)")]
		public double Weight { get; set; }

		[Required]
		[MinLength(CoachPhoneNumberMinLength)]
		[MaxLength(CoachPhoneNumberMaxLength)]
		[Phone]
		[Display(Name = "Your Phone")]
		public string PhoneNumber { get; set; } = null!;

		[MaxLength(CoachDescriptionMaxLength)]
		[Description]
		[Display(Name = "Description")]
		public string? Description { get; set; }

		[Display(Name = "Category")]
		public int CoachCategoryId { get; set; }
		public IEnumerable<CoachSelectCategoryFormModel> CoachCategories { get; set; } = new List<CoachSelectCategoryFormModel>();
	}
}
