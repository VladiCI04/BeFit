using BeFit.Data.Models;
using BeFit.Web.ViewModels.Coach;
using System.ComponentModel.DataAnnotations;

namespace BeFit.Web.ViewModels.Event
{
	public class EventDetailsViewModel : EventAllViewModel
	{
		public string Description { get; set; } = null!;

		[Display(Name = "Event Category")]
		public string Category { get; set; } = null!;

		public DateTime Start { get; set; }

		public DateTime End { get; set; } 

		[Display(Name = "Clients")]
		public List<ApplicationUser> Clients { get; set; } = new List<ApplicationUser>();

		public CoachInfoOnEventViewModel Coach { get; set; } = null!;
    }
}
