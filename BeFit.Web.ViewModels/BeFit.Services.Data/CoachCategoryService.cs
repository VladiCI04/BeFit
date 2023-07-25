using BeFit.Data;
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

		public async Task<IEnumerable<string>> AllCoachCategoryNamesAsync()
		{
			IEnumerable<string> allNames = await this.dbContext
				.CoachCategories
				.Select(cc => cc.Name)
				.ToArrayAsync();

			return allNames;
		}
	}
}
