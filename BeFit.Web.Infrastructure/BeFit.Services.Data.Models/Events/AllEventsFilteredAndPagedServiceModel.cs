using BeFit.Web.ViewModels.Event;

namespace BeFit.Services.Data.Models.Events
{
	public class AllEventsFilteredAndPagedServiceModel
	{
        public int TotalEventsCount { get; set; }

        public IEnumerable<EventAllViewModel> Events { get; set; } = new List<EventAllViewModel>();
    }
}
