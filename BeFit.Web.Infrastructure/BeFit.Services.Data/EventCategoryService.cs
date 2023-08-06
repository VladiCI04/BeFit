using BeFit.Data;
using BeFit.Data.Models;
using BeFit.Services.Data.Interfaces;
using BeFit.Web.ViewModels.EventCategory;
using Microsoft.EntityFrameworkCore;

namespace BeFit.Services.Data
{
    public class EventCategoryService : IEventCategoryService
    {
        private readonly BeFitDbContext dbContext;

        public EventCategoryService(BeFitDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<EventSelectCategoryFormModel>> AllEventCategoriesAsync()
        {
            IEnumerable<EventSelectCategoryFormModel> allEventCategories = await this.dbContext
                .EventCategories
                .AsNoTracking()
                .Select(ec => new EventSelectCategoryFormModel()
                {
                    Id = ec.Id,
                    Name = ec.Name
                })
                .ToArrayAsync();

            return allEventCategories;
        }

        public async Task<IEnumerable<AllEventCategoriesViewModel>> AllEventCategoriesForListAsync()
        {
            IEnumerable<AllEventCategoriesViewModel> allEventCategories = await this.dbContext
                .EventCategories
                .AsNoTracking()
                .Select(ec => new AllEventCategoriesViewModel()
                {
                    Id = ec.Id,
                    Name = ec.Name
                })
                .ToArrayAsync();

            return allEventCategories;
        }

        public async Task<bool> ExistsByIdAsync(int id)
		{
            bool result = await this.dbContext
                .EventCategories
                .AnyAsync(c => c.Id == id);

            return result;
		}

		public async Task<IEnumerable<string>> AllEventCategoryNamesAsync()
		{
			IEnumerable<string> allNames = await this.dbContext
                .EventCategories
                .Select(ec => ec.Name)
                .ToArrayAsync();

            return allNames;
		}

		public async Task<EventCategoryDetailsViewModel> GetDetailsByIdAsync(int id)
		{
            EventCategory eventCategory = await this.dbContext
                .EventCategories
                .FirstAsync(ec => ec.Id == id);

			EventCategoryDetailsViewModel viewModel = new EventCategoryDetailsViewModel()
            {
                Id = eventCategory.Id,
                Name = eventCategory.Name
            };

            return viewModel;
		}
	}
}
