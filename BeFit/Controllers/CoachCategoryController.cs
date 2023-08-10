using BeFit.Services.Data.Interfaces;
using BeFit.Web.Infrastructure.Extensions;
using BeFit.Web.ViewModels.CoachCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeFit.Controllers
{
	public class CoachCategoryController : Controller
	{
		private readonly ICoachCategoryService coachCategoryService;

        public CoachCategoryController(ICoachCategoryService coachCategoryService)
        {
				this.coachCategoryService = coachCategoryService;
        }

		[HttpGet]
        public async Task<IActionResult> All()
		{
			IEnumerable<AllCoachCategoriesViewModel> viewModel = await this.coachCategoryService.AllCoachCategoriesForListAsync();

			return this.View(viewModel);
		}

		[HttpGet]
		public async Task<IActionResult> Details(int id, string information)
		{
			bool coachCategoryExists = await this.coachCategoryService.ExistsByIdAsync(id);
			if (!coachCategoryExists) 
			{ 
				return this.NotFound();
			}

			CoachCategoryDetailsViewModel viewModel = await this.coachCategoryService.GetDetailsByIdAsync(id);		
			if (viewModel.GetUrlInformationForCoach() != information) 
			{ 
				return this.NotFound();
			}

			return this.View(viewModel);
		}
	}
}
