﻿using BeFit.Services.Data.Models.Events;
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

        Task<bool> ExestsByIdAsync(string eventId);

        Task<EventDetailsViewModel?> GetDetailsByIdAsync(string eventId);

        Task<EventFormModel> GetEventForEditByIdAsync(string eventId);

        Task<bool> IsCoachWithIdOwnerOfEventWithIdAsync(string eventId, string coachId);

        Task EditEventByIdAndFormModel(string eventId, EventFormModel formModel);
    }
}
