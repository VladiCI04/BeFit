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

        public EventController(IEventCategoryService eventCategoryService, ICoachService coachService)
        {
            this.eventCategoryService = eventCategoryService;
            this.coachService = coachService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            return View();
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
    }
}
