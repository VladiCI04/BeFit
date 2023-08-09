using BeFit.Web.ViewModels.Event;

namespace BeFit.Areas.Admin.ViewModels.Event
{
    public class MyEventsViewModel
    {
        public IEnumerable<EventAllViewModel> AddedEvents { get; set; } = new List<EventAllViewModel>();

        public IEnumerable<EventAllViewModel> JoinedEvents { get; set; } = new List<EventAllViewModel>();
    }
}
