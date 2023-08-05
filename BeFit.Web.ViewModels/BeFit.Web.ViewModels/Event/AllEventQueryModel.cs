using System.ComponentModel.DataAnnotations;
using BeFit.Web.ViewModels.Event.Enums;
using static BeFit.Common.GeneralApplicationConstants;

namespace BeFit.Web.ViewModels.Event
{
	public class AllEventsQueryModel
	{
		[Display(Name = "Category")]
        public string? EventCategory { get; set; }

		[Display(Name = "Search by word")]
		public string? SearchString { get; set; }

		[Display(Name = "Sort Events by")]
		public EventSorting EventSorting { get; set; }

        public int CurrentPage { get; set; } = DefaultPage;

		[Display(Name = "On Page")]
        public int EventsPerPage { get; set; } = EntitiesPerPage;

        public int TotalEvents { get; set; }

        public IEnumerable<string> EventCategories { get; set; } = new List<string>();

		public IEnumerable<EventAllViewModel> Events { get; set; } = new List<EventAllViewModel>();
	}
}
