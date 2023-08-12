using BeFit.Areas.Admin.ViewModels.Event;
using BeFit.Services.Data.Interfaces;
using BeFit.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BeFit.Areas.Admin.Controllers
{
    public class EventController : BaseController
	{
		private readonly ICoachService coachService;
		private readonly IEventService eventService;

        public EventController(ICoachService coachService, IEventService eventService)
        {
			this.coachService = coachService;
			this.eventService = eventService;
        }

        public async Task<IActionResult> Mine()
		{
			string coachId = await this.coachService.GetCoachIdByUserIdAsync(this.User.GetId()!);
			MyEventsViewModel viewModel = new MyEventsViewModel()
			{
				AddedEvents = await this.eventService.AllByCoachIdAsync(coachId),
				JoinedEvents = await this.eventService.AllByUserIdAsync(this.User.GetId()!)	
			};

			return this.View(viewModel);
		}
	}
}
