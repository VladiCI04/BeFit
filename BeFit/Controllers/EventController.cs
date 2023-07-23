using BeFit.Services.Data.Interfaces;
using BeFit.Web.Infrastructure.Extensions;
using BeFit.Web.ViewModels.Event;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BeFit.Common.NotificationMessagesConstants;

namespace BeFit.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly IEventCategoryService eventCategoryService;
        private readonly ICoachService coachService;
        private readonly IEventService eventService;

        public EventController(IEventCategoryService eventCategoryService, ICoachService coachService, IEventService eventService)
        {
            this.eventCategoryService = eventCategoryService;
            this.coachService = coachService;
            this.eventService = eventService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            return this.Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            bool isCoach = await this.coachService.CoachExistsByUserIdAsync(this.User.GetId()!);
            if (!isCoach)
            {
                this.TempData[ErrorMessage] = "You must become a coach in order to add new event!";

                return this.RedirectToAction("Become", "Coach");
            }

            EventFormModel formModel = new EventFormModel()
            {
                EventCategories = await this.eventCategoryService.AllEventCategoriesAsync()
            };

            return View(formModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(EventFormModel model)
        {
			bool isCoach = await this.coachService.CoachExistsByUserIdAsync(this.User.GetId()!);
			if (!isCoach)
			{
				this.TempData[ErrorMessage] = "You must become a coach in order to add new event!";

				return this.RedirectToAction("Become", "Coach");
			}

            bool eventCategoryExists = await this.eventCategoryService.ExistsByIdAsync(model.EventCategoryId);
            if (!eventCategoryExists)
            {
                this.ModelState.AddModelError(nameof(model.EventCategoryId), "Selected category does not exist!");
            }

            if (!this.ModelState.IsValid)
            {
                model.EventCategories = await this.eventCategoryService.AllEventCategoriesAsync();

                return this.View(model);
            }

            try
            {
                string coachId = await this.coachService.GetCoachIdByUserIdAsync(this.User.GetId()!);
                await this.eventService.CreateAsync(model, coachId); 
            }
            catch (Exception _)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occured while trying to add your new event! Please try again later or contact administarator!");

                model.EventCategories = await this.eventCategoryService.AllEventCategoriesAsync();

                return this.View(model);
            }

            return this.RedirectToAction("All", "Event");
		}
    }
}
