using BeFit.Data;
using BeFit.Services.Data;
using BeFit.Services.Data.Interfaces;
using BeFit.Web.ViewModels.EventCategory;
using Microsoft.EntityFrameworkCore;
using static BeFit.Services.Tests.DatabaseSeeder;

namespace BeFit.Services.Tests
{
    public class EventCategoryServiceTests
    {
        private DbContextOptions<BeFitDbContext> dbOptions;
        private BeFitDbContext dbContext;
        private IEventCategoryService eventCategoryService;

        [OneTimeSetUp]
        public void OnTimeSetUp() 
        {
            this.dbOptions = new DbContextOptionsBuilder<BeFitDbContext>()
                .UseInMemoryDatabase("BeFitInMemory" + Guid.NewGuid().ToString())
                .Options;
            this.dbContext = new BeFitDbContext(this.dbOptions, false);

            this.dbContext.Database.EnsureCreated();
            SeedDtabase(this.dbContext);

            this.eventCategoryService = new EventCategoryService(this.dbContext);
        }

        // AllEventCategoriesAsync Test
        [Test]
        public async Task AllEventCategoriesAsyncShouldReturnAllEventCategories()
        {
            IEnumerable<EventSelectCategoryFormModel> actualAllEventCategories = await this.dbContext
               .EventCategories
               .AsNoTracking()
               .Select(ec => new EventSelectCategoryFormModel()
               {
                   Id = ec.Id,
                   Name = ec.Name
               })
               .ToArrayAsync();

            IEnumerable<EventSelectCategoryFormModel> expectedAllEventCategories = await this.eventCategoryService.AllEventCategoriesAsync();

            Assert.IsTrue(expectedAllEventCategories.Count() == actualAllEventCategories.Count());
        }

        // AllEventCategoriesForListAsync Test
        [Test]
        public async Task AllEventCategoriesForListAsyncShouldReturnAllEventCategories()
        {
            IEnumerable<AllEventCategoriesViewModel> actualAllEventCategories = await this.dbContext
                .EventCategories
                .AsNoTracking()
                .Select(cc => new AllEventCategoriesViewModel()
                {
                    Id = cc.Id,
                    Name = cc.Name
                })
                .ToArrayAsync();

            IEnumerable<AllEventCategoriesViewModel> expectedAllEventCategories = await this.eventCategoryService.AllEventCategoriesForListAsync();

            Assert.IsTrue(expectedAllEventCategories.Count() == actualAllEventCategories.Count());
        }

        // ExistsByIdAsync Test
        [Test]
        public async Task ExistsByIdAsyncShouldReturnTrueIfExist()
        {
            int existedEventCategoryId = EventCategory.Id;  

            bool result = await this.eventCategoryService.ExistsByIdAsync(existedEventCategoryId);

            Assert.IsTrue(result);
        }
        [Test]
        public async Task ExistsByIdAsyncShouldReturnFalseIfNotExist()
        {
            int existedEventCategoryId = 2;

            bool result = await this.eventCategoryService.ExistsByIdAsync(existedEventCategoryId);

            Assert.IsFalse(result);
        }

        // AllEventCategoryNamesAsync Test
        [Test]
        public async Task AllEventCategoryNamesAsyncShouldReturnCategoriesNames()
        {
            IEnumerable<string> actualAllNames = await this.dbContext
                .EventCategories
                .Select(cc => cc.Name)
                .ToArrayAsync();

            IEnumerable<string> expectedAllNames = await this.eventCategoryService.AllEventCategoryNamesAsync();

            Assert.IsTrue(actualAllNames.Count() == expectedAllNames.Count());
        }

        // GetDetailsByIdAsync Test
        [Test]
        public async Task GetDetailsByIdAsyncShouldReturnCorrectViewModel()
        {
            EventCategoryDetailsViewModel actualViewModel = new EventCategoryDetailsViewModel()
            {
                Id = CoachCategory.Id,
                Name = CoachCategory.Name,
                Events = this.dbContext.Events.Count(c => c.EventCategoryId == EventCategory.Id)
            };

            EventCategoryDetailsViewModel expectedViewModel = await eventCategoryService.GetDetailsByIdAsync(EventCategory.Id);

            Assert.IsTrue(expectedViewModel.Id == actualViewModel.Id && expectedViewModel.Name == actualViewModel.Name && expectedViewModel.Events == actualViewModel.Events);
        }
        [Test]
        public void GetDetailsByNameAsyncShouldReturnExceptionIfCategoryIdNotExist()
        {
            int eventCategoryId = 2;

            Assert.ThrowsAsync<InvalidOperationException>(async () => await this.eventCategoryService.GetDetailsByIdAsync(eventCategoryId));
        }
    }
}