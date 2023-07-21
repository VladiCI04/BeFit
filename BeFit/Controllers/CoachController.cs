using BeFit.Services.Data.Interfaces;
using BeFit.Web.Infrastructure.Extensions;
using BeFit.Web.ViewModels.Coach;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BeFit.Common.NotificationMessagesConstants;

namespace BeFit.Controllers
{
    [Authorize]
    public class CoachController : Controller
    {
        private readonly ICoachService coachService;

        public CoachController(ICoachService coachService)
        {
            this.coachService = coachService;
        }

        [HttpGet]
        public async Task<IActionResult> Become()
        {
            string? userId = this.User.GetId();
            bool isAgent = await this.coachService.CoachExistsByUserIdAsync(userId);

            if (isAgent)
            {
                TempData[ErrorMessage] = "You are already a coach!";

                return this.RedirectToAction("Index", "Home");
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Become(BecomeCoachFormModel model)
        {
			string? userId = this.User.GetId();
			bool isAgent = await this.coachService.CoachExistsByUserIdAsync(userId);

			if (isAgent)
			{
				this.TempData[ErrorMessage] = "You are already a coach!";

				return this.RedirectToAction("Index", "Home");
			}

            bool isPhoneNumberTaken =
                await this.coachService.CoachExistsByPhoneNumberAsync(model.PhoneNumber);
            if (isPhoneNumberTaken)
            {
                this.ModelState.AddModelError(nameof(model.PhoneNumber), "Coach with the provided phone number already exists!");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            bool userHasActiveEvents = await this.coachService
                .HasEventsByUserIdAsync(userId);
            if (userHasActiveEvents)
            {
                this.TempData[ErrorMessage] = "You must not have any active events in order to become a coach!";

                return this.RedirectToAction("Mine", "Event");
            }

            try
            {
				await this.coachService.Create(userId, model);
			}
            catch (Exception)
            {
                this.TempData[ErrorMessage] = 
                    "Unexpected error occured while registering you as a coach! Please train again later or contact administrator.";

                return this.RedirectToAction("Index", "Home");
            }

            return this.RedirectToAction("All", "Event");
		}
    }
}
