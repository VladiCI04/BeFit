using BeFit.Services.Data.Models.Events;
using BeFit.Web.ViewModels.Event;
using BeFit.Web.ViewModels.Home;

namespace BeFit.Services.Data.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<IndexViewModel>> AllEventsAsync();

        Task CreateAsync(EventFormModel formModel, string coachId);

        Task<AllEventsFilteredAndPagedServiceModel> AllAsync(AllEventsQueryModel queryModel);

        Task<IEnumerable<EventAllViewModel>> AllByCoachIdAsync(string coachId);

        Task<IEnumerable<EventAllViewModel>> AllByUserIdAsync(string userId);
    }
}
