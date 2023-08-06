using BeFit.Web.ViewModels.CoachCategory.Interfaces;
using BeFit.Web.ViewModels.EventCategory.Interfaces;

namespace BeFit.Web.Infrastructure.Extensions
{
	public static class ViewModelsExtensions
	{
		public static string GetUrlInformationForEvent(this IEventCategoryDetailsModel model)
		{
			return model.Name.Replace(" ", "-");
		}

		public static string GetUrlInformationForCoach(this ICoachCategoryDetailsModel model)
		{
			return model.Name.Replace(" ", "-");
		}
	}
}
