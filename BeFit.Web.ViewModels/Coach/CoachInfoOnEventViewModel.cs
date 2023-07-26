using System.ComponentModel.DataAnnotations;

namespace BeFit.Web.ViewModels.Coach
{
	public class CoachInfoOnEventViewModel
	{
		public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

		[Display(Name = "Phone")]
		public string PhoneNumber { get; set; } = null!;

		[Display(Name = "Coach Category")]
		public int Category { get; set; }
	}
}
