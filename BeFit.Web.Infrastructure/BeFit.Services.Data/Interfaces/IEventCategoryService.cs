using BeFit.Web.ViewModels.EventCategory;

namespace BeFit.Services.Data.Interfaces
{
    public interface IEventCategoryService
    {
        Task<IEnumerable<EventSelectCategoryFormModel>> AllEventCategoriesAsync();

        Task<bool> ExistsByIdAsync(int id);

        Task<IEnumerable<string>> AllEventCategoryNamesAsync();
    }
}
