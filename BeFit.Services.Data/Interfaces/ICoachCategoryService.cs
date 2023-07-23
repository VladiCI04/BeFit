using BeFit.Web.ViewModels.CoachCategory;

namespace BeFit.Services.Data.Interfaces
{
	public interface ICoachCategoryService
	{
		Task<IEnumerable<CoachSelectCategoryFormModel>> AllCoachCategoriesAsync();
	}
}
