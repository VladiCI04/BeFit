using BeFit.Data;
using BeFit.Data.Models;
using BeFit.Services.Data.Interfaces;
using BeFit.Web.ViewModels.CoachCategory;
using Microsoft.EntityFrameworkCore;

namespace BeFit.Services.Data
{
	public class CoachCategoryService : ICoachCategoryService
	{
		private readonly BeFitDbContext dbContext;

        public CoachCategoryService(BeFitDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<CoachSelectCategoryFormModel>> AllCoachCategoriesAsync()
		{
			IEnumerable<CoachSelectCategoryFormModel> allCoachCategories = await this.dbContext
			   .CoachCategories
			   .AsNoTracking()
			   .Select(ec => new CoachSelectCategoryFormModel()
			   {
				   Id = ec.Id,
				   Name = ec.Name
			   })
			   .ToArrayAsync();

			return allCoachCategories;
		}

		public async Task<IEnumerable<AllCoachCategoriesViewModel>> AllCoachCategoriesForListAsync()
		{
			IEnumerable<AllCoachCategoriesViewModel> allCoachCategories = await this.dbContext
				.CoachCategories
				.AsNoTracking()
				.Select(cc => new AllCoachCategoriesViewModel()
				{
					Id = cc.Id,
					Name = cc.Name
				})
				.ToArrayAsync();

			return allCoachCategories;
		}

		public async Task<bool> ExistsByIdAsync(int id)
		{
			bool result = await this.dbContext
				.CoachCategories
				.AnyAsync(c => c.Id == id);

			return result;
		}

		public async Task<IEnumerable<string>> AllCoachCategoryNamesAsync()
		{
			IEnumerable<string> allNames = await this.dbContext
				.CoachCategories
				.Select(cc => cc.Name)
				.ToArrayAsync();

			return allNames;
		}

		public async Task<CoachCategoryDetailsViewModel> GetDetailsByIdAsync(int id)
		{
			CoachCategory coachCategory = await this.dbContext
				.CoachCategories
				.FirstAsync(cc => cc.Id == id);

			CoachCategoryDetailsViewModel viewModel = new CoachCategoryDetailsViewModel()
			{
				Id = coachCategory.Id,
				Name = coachCategory.Name,
				Coaches = this.dbContext.Coaches.Count(c => c.CoachCategoryId == id)
            };

			return viewModel;
		}
	}
}
