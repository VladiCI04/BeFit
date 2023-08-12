using BeFit.Web.ViewModels.EventCategory.Interfaces;

namespace BeFit.Web.ViewModels.EventCategory
{
	public class EventCategoryDetailsViewModel : IEventCategoryDetailsModel
	{
		public int Id { get; set; }

		public string Name { get; set; } = null!;

		public int Events { get; set; } = 0;
	}
}
