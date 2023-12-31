﻿using BeFit.Services.Data.Interfaces;
using BeFit.Services.Data.Models.Events;
using BeFit.Web.Infrastructure.Extensions;
using BeFit.Web.ViewModels.Event;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BeFit.Common.GeneralApplicationConstants;
using static BeFit.Common.NotificationMessagesConstants;

namespace BeFit.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly IEventService eventService;
        private readonly IEventCategoryService eventCategoryService;
		private readonly ICoachService coachService;

        public EventController(IEventCategoryService eventCategoryService, IEventService eventService, ICoachService coachService)
        {
			this.eventService = eventService;
            this.eventCategoryService = eventCategoryService;
            this.coachService = coachService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery]AllEventsQueryModel queryModel)
        {
            AllEventsFilteredAndPagedServiceModel serviceModel = 
                await this.eventService.AllAsync(queryModel);

            queryModel.Events = serviceModel.Events;
            queryModel.TotalEvents = serviceModel.TotalEventsCount;
            queryModel.EventCategories = await this.eventCategoryService.AllEventCategoryNamesAsync();

            return this.View(queryModel);
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

            try
            {
				EventFormModel formModel = new EventFormModel()
				{
					EventCategories = await this.eventCategoryService.AllEventCategoriesAsync()
				};

				return this.View(formModel);
			}
            catch (Exception)
            {
                return this.GeneralError();
			}
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
				string eventId = await this.eventService.CreateAndReturnIdAsync(model, coachId);

                this.TempData[SuccessMessage] = "Event was added succesfully!";
				return this.RedirectToAction("Details", "Event", new { id = eventId });
			}
			catch (Exception)
			{
				this.ModelState.AddModelError(string.Empty, "Unexpected error occured while trying to add your new event! Please try again later or contact administarator!");

				model.EventCategories = await this.eventCategoryService.AllEventCategoriesAsync();

				return this.View(model);
			}
		}

		[HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            bool eventExists = await this.eventService
                .ExistsByIdAsync(id);
            if (!eventExists)
            {
                this.TempData[ErrorMessage] = "Event with the provided id does not exist!";

                return this.RedirectToAction("All", "Event");
            }

            try
            {
				EventDetailsViewModel? viewModel = await this.eventService.GetDetailsByIdAsync(id);

				return this.View(viewModel);
			}
            catch (Exception)
            {
				return this.GeneralError();
			}
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            bool eventExists = await this.eventService
                .ExistsByIdAsync(id);
            if (!eventExists)
            {
                this.TempData[ErrorMessage] = "Event with the provided id does not exist!";

                return this.RedirectToAction("All", "Event");
            }

            bool isUserCoach = await this.coachService
                .CoachExistsByUserIdAsync(this.User.GetId()!);
            if (!isUserCoach && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "You must become a coach in order to edit event info!";

                return this.RedirectToAction("Become", "Coach");
            }

            string? coachId = await this.coachService.GetCoachIdByUserIdAsync(this.User.GetId()!);
            bool isCoachOwner = await this.eventService.IsCoachWithIdOwnerOfEventWithIdAsync(id, coachId!);
            if (!isCoachOwner && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "You must be the coach owner of the event you want to edit!";

                return this.RedirectToAction("Mine", "Event");
            }

            try
            {
				EventFormModel formModel = await this.eventService.GetEventForEditByIdAsync(id);

				formModel.EventCategories = await this.eventCategoryService.AllEventCategoriesAsync();

				return this.View(formModel);
			}
            catch (Exception)
            {
				return this.GeneralError();
			}
        }

		[HttpPost]
		public async Task<IActionResult> Edit(string id, EventFormModel model)
		{
            if (!this.ModelState.IsValid)
            {
                model.EventCategories = await this.eventCategoryService.AllEventCategoriesAsync();

                return this.View(model);
            }

			bool eventExists = await this.eventService
				.ExistsByIdAsync(id);
			if (!eventExists)
			{
				this.TempData[ErrorMessage] = "Event with the provided id does not exist!";

				return this.RedirectToAction("All", "Event");
			}

			bool isUserCoach = await this.coachService
				.CoachExistsByUserIdAsync(this.User.GetId()!);
			if (!isUserCoach && !this.User.IsAdmin())
			{
				this.TempData[ErrorMessage] = "You must become a coach in order to edit event info!";

				return this.RedirectToAction("Become", "Coach");
			}

			string coachId = await this.coachService.GetCoachIdByUserIdAsync(this.User.GetId()!);
			bool isCoachOwner = await this.eventService.IsCoachWithIdOwnerOfEventWithIdAsync(id, coachId!);
			if (!isCoachOwner && !this.User.IsAdmin())
			{
				this.TempData[ErrorMessage] = "You must be the coach owner of the event you want to edit!";

				return this.RedirectToAction("Mine", "Event");
			}

            try
            {
                await this.eventService.EditEventByIdAndFormModelAsync(id, model);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occured while trying to update the event. Please try again later or contact administrator!");

                model.EventCategories = await this.eventCategoryService.AllEventCategoriesAsync();

                return this.View(model);
            }

			this.TempData[SuccessMessage] = "Event was edited succesfully!";
			return this.RedirectToAction("Details", "Event", new { id });
		}

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
			bool eventExists = await this.eventService
				.ExistsByIdAsync(id);
			if (!eventExists)
			{
				this.TempData[ErrorMessage] = "Event with the provided id does not exist!";

				return this.RedirectToAction("All", "Event");
			}

			bool isUserCoach = await this.coachService
				.CoachExistsByUserIdAsync(this.User.GetId()!);
			if (!isUserCoach && !this.User.IsAdmin())
			{
				this.TempData[ErrorMessage] = "You must become a coach in order to edit event info!";

				return this.RedirectToAction("Become", "Coach");
			}

			string coachId = await this.coachService.GetCoachIdByUserIdAsync(this.User.GetId()!);
			bool isCoachOwner = await this.eventService.IsCoachWithIdOwnerOfEventWithIdAsync(id, coachId);
			if (!isCoachOwner && !this.User.IsAdmin())
			{
				this.TempData[ErrorMessage] = "You must be the coach owner of the event you want to edit!";

				return this.RedirectToAction("Mine", "Event");
			}

			try
			{
				EventPreDeleteDetailsViewModel viewModel = await this.eventService.GetEventForDeleteByIdAsync(id);

				return this.View(viewModel);
			}
			catch (Exception)
			{
				return this.GeneralError();
			}
		}

		[HttpPost]
		public async Task<IActionResult> Delete(string id, EventPreDeleteDetailsViewModel model)
		{
			bool eventExists = await this.eventService
			.ExistsByIdAsync(id);
			if (!eventExists)
			{
				this.TempData[ErrorMessage] = "Event with the provided id does not exist!";

				return this.RedirectToAction("All", "Event");
			}

			bool isUserCoach = await this.coachService
				.CoachExistsByUserIdAsync(this.User.GetId()!);
			if (!isUserCoach && !this.User.IsAdmin())
			{
				this.TempData[ErrorMessage] = "You must become a coach in order to edit event info!";

				return this.RedirectToAction("Become", "Coach");
			}

			string? coachId = await this.coachService.GetCoachIdByUserIdAsync(this.User.GetId()!);
			bool isCoachOwner = await this.eventService.IsCoachWithIdOwnerOfEventWithIdAsync(id, coachId!);
			if (!isCoachOwner && !this.User.IsAdmin())
			{
				this.TempData[ErrorMessage] = "You must be the coach owner of the event you want to edit!";

				return this.RedirectToAction("Mine", "Event");
			}

			try
			{
				await this.eventService.DeleteEventByIdAsync(id);
				this.TempData[WarningMessage] = "The event was successfully deleted!";

				return RedirectToAction("Mine", "Event");
			}
			catch (Exception)
			{
				return this.GeneralError();
			}
		}

		[HttpPost]
		public async Task<IActionResult> Join(string id)
		{
			EventDetailsViewModel? even = await eventService.GetEventDetailsByIdAsync(id);
			if (even == null)
			{
				return this.RedirectToAction("All", "Event");
			}

			string userId = this.User.GetId()!;
            bool isUserCoach = await this.coachService
				.CoachExistsByUserIdAsync(userId);
            if (isUserCoach && !this.User.IsAdmin())
            {
				this.TempData[WarningMessage] = "You are coach and can't join in events!";

				return this.RedirectToAction("All", "Event");
			}

            bool alreadyAdded = await this.eventService.AddEventToMineAsync(userId, even);
			if (alreadyAdded)
			{
				TempData[SuccessMessage] = "You join in this event successfully!";
                return this.RedirectToAction("Mine", "Event");
            }
			else
			{
				TempData[WarningMessage] = "You already joined in this event!";
                return this.RedirectToAction("All", "Event");
            }			
		}

		[HttpPost]
		public async Task<IActionResult> Leave(string id)
		{
			EventDetailsViewModel? even = await this.eventService.GetEventDetailsByIdAsync(id);
			if (even == null)
			{
				return this.RedirectToAction("All", "Event");
			}

			string userId = this.User.GetId()!;
			await eventService.RemoveEventFromMineAsync(userId, even);

			TempData[SuccessMessage] = "You have successfully left a event!";
			return this.RedirectToAction("All", "Event");
		}

		[HttpGet]
        public async Task<IActionResult> Mine()
        {
			if (this.User.IsInRole(AdminRoleName))
			{
				return this.RedirectToAction("Mine", "Event", new { Area = AdminAreaName });
			}

            List<EventAllViewModel> myEvents = new List<EventAllViewModel>();

            string userId = this.User.GetId()!;
            bool isUserCoach = await this.coachService
                .CoachExistsByUserIdAsync(userId);

            try
            {
				if (this.User.IsAdmin()) 
				{
                    string? coachId = await this.coachService.GetCoachIdByUserIdAsync(userId);

                    myEvents.AddRange(await this.eventService.AllByCoachIdAsync(coachId));

                    myEvents.AddRange(await this.eventService.AllByUserIdAsync(userId));

					myEvents = myEvents
						.DistinctBy(e => e.Id)
						.ToList();
                }
				else if (isUserCoach)
				{
					string? coachId = await this.coachService.GetCoachIdByUserIdAsync(userId);

					myEvents.AddRange(await this.eventService.AllByCoachIdAsync(coachId));
				}
				else
				{
					myEvents.AddRange(await this.eventService.AllByUserIdAsync(userId));
				}

				return this.View(myEvents);
			}
            catch (Exception)
            {
				return this.GeneralError();
			}
        }

        private IActionResult GeneralError()
        {
			this.TempData[ErrorMessage] = "Unexpected error occured! Please try again later or contact administrator!";

			return this.RedirectToAction("Index", "Home");
		}
    }
}
