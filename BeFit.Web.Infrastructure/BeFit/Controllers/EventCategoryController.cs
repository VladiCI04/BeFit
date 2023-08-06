using BeFit.Services.Data.Interfaces;
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

        public async Task<IActionResult> All()
        {
            IEnumerable<AllEventCategoriesViewModel> viewModel = await this.eventCategoryService.AllEventCategoriesForListAsync();

            return this.View(viewModel);
        }
    }
}
