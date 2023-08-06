using BeFit.Services.Data.Interfaces;
using BeFit.Web.Infrastructure.Extensions;
using BeFit.Web.ViewModels.EventCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeFit.Controllers
{
    [Authorize]
    public class EventCategoryController : Controller
    {
        private readonly IEventCategoryService eventCategoryService;

        public EventCategoryController(IEventCategoryService eventCategoryService)
        {
               this.eventCategoryService = eventCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            IEnumerable<AllEventCategoriesViewModel> viewModel = await this.eventCategoryService.AllEventCategoriesForListAsync();

            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id, string information)
        {
            bool eventCategoryExists = await this.eventCategoryService.ExistsByIdAsync(id);

            if (!eventCategoryExists) 
            { 
                return NotFound();
            }

            EventCategoryDetailsViewModel viewModel = await this.eventCategoryService.GetDetailsByIdAsync(id);

            if (viewModel.GetUrlInformationForEvent() != information)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }
    }
}
