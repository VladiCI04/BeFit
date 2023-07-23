using BeFit.Web.ViewModels.EventCategory;

namespace BeFit.Services.Data.Interfaces
{
    public interface IEventCategoryService
    {
        Task<IEnumerable<EventSelectCategoryFormModel>> AllEventCategoriesAsync();
    }
}
