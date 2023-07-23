using BeFit.Data;
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
    }
}
