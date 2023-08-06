using BeFit.Web.ViewModels.EventCategory.Interfaces;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BeFit.Web.Infrastructure.Extensions
{
	public static class ViewModelsExtensions
	{
		public static string GetUrlInformation(this IEventCategoryDetailsModel model)
		{
			return model.Name.Replace(" ", "-");
		}
	}
}
