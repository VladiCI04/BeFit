using BeFit.Data.Models;
using BeFit.Services.Data.Models.Events;
using BeFit.Services.Data.Models.Statistics;
using BeFit.Web.ViewModels.Event;
using BeFit.Web.ViewModels.Home;

namespace BeFit.Services.Data.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<IndexViewModel>> AllEventsAsync();

        Task<string> CreateAndReturnIdAsync(EventFormModel formModel, string coachId);

        Task<AllEventsFilteredAndPagedServiceModel> AllAsync(AllEventsQueryModel queryModel);

        Task<IEnumerable<EventAllViewModel>> AllByCoachIdAsync(string coachId);

        Task<IEnumerable<EventAllViewModel>> AllByUserIdAsync(string userId);

        Task<bool> ExistsByIdAsync(string eventId);

        Task<EventDetailsViewModel?> GetDetailsByIdAsync(string eventId);

        Task<EventFormModel> GetEventForEditByIdAsync(string eventId);

        Task<bool> IsCoachWithIdOwnerOfEventWithIdAsync(string eventId, string coachId);

        Task EditEventByIdAndFormModelAsync(string eventId, EventFormModel formModel);

        Task<EventPreDeleteDetailsViewModel> GetEventForDeleteByIdAsync(string eventId);

        Task DeleteEventByIdAsync(string eventId);

        Task AddEventToMineAsync(string userId, EventDetailsViewModel even);

        Task<EventDetailsViewModel?> GetEventDetailsByIdAsync(string id);

        Task<Event> GetEventByIdAsync(string id);

        Task RemoveEventFromMineAsync(string userId, EventDetailsViewModel even);

        Task<StatisticsServiceModel> GetStatisticsAsync();
	}
}
