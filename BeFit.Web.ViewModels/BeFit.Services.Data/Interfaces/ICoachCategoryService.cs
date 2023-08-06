using BeFit.Web.ViewModels.CoachCategory;

namespace BeFit.Services.Data.Interfaces
{
	public interface ICoachCategoryService
	{
		Task<IEnumerable<CoachSelectCategoryFormModel>> AllCoachCategoriesAsync();

		Task<IEnumerable<AllCoachCategoriesViewModel>> AllCoachCategoriesForListAsync();

		Task<bool> ExistsByIdAsync(int id);

		Task<IEnumerable<string>> AllCoachCategoryNamesAsync();

		Task<CoachCategoryDetailsViewModel> GetDetailsByIdAsync(int id);
	}
}
