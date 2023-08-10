using BeFit.Data;
using BeFit.Services.Data;
using BeFit.Services.Data.Interfaces;
using BeFit.Web.ViewModels.CoachCategory;
using Microsoft.EntityFrameworkCore;
using static BeFit.Services.Tests.DatabaseSeeder;

namespace BeFit.Services.Tests
{
    public class CoachCategoryServiceTests
    {
        private DbContextOptions<BeFitDbContext> dbOptions;
        private BeFitDbContext dbContext;
        private ICoachCategoryService coachCategoryService;

        [OneTimeSetUp]
        public void OnTimeSetUp() 
        {
            this.dbOptions = new DbContextOptionsBuilder<BeFitDbContext>()
                .UseInMemoryDatabase("BeFitInMemory" + Guid.NewGuid().ToString())
                .Options;
            this.dbContext = new BeFitDbContext(this.dbOptions);

            this.dbContext.Database.EnsureCreated();
            SeedDtabase(this.dbContext);

            this.coachCategoryService = new CoachCategoryService(this.dbContext);
        }

        // AllCoachCategoriesAsync Test
        [Test]
        public async Task AllCoachCategoriesAsyncShouldReturnAllCoachCategories()
        {
            IEnumerable<CoachSelectCategoryFormModel> actualAllCoachCategories = await this.dbContext
               .CoachCategories
               .AsNoTracking()
               .Select(ec => new CoachSelectCategoryFormModel()
               {
                   Id = ec.Id,
                   Name = ec.Name
               })
               .ToArrayAsync();

            IEnumerable<CoachSelectCategoryFormModel> expectedAllCoachCategories = await this.coachCategoryService.AllCoachCategoriesAsync();

            Assert.IsTrue(expectedAllCoachCategories.Count() == actualAllCoachCategories.Count());
        }

        // AllCoachCategoriesForListAsync Test
        [Test]
        public async Task AllCoachCategoriesForListAsyncShouldReturnAllCoachCategories()
        {
            IEnumerable<AllCoachCategoriesViewModel> actualAllCoachCategories = await this.dbContext
                .CoachCategories
                .AsNoTracking()
                .Select(cc => new AllCoachCategoriesViewModel()
                {
                    Id = cc.Id,
                    Name = cc.Name
                })
                .ToArrayAsync();

            IEnumerable<AllCoachCategoriesViewModel> expectedAllCoachCategories = await this.coachCategoryService.AllCoachCategoriesForListAsync();

            Assert.IsTrue(expectedAllCoachCategories.Count() == actualAllCoachCategories.Count());
        }

        // ExistsByIdAsync Test
        [Test]
        public async Task ExistsByIdAsyncShouldReturnTrueIfExist()
        {
            int existedCoachCategoryId = CoachCategory.Id;  

            bool result = await this.coachCategoryService.ExistsByIdAsync(existedCoachCategoryId);

            Assert.IsTrue(result);
        }
        [Test]
        public async Task ExistsByIdAsyncShouldReturnFalseIfNotExist()
        {
            int existedCoachCategoryId = 2;

            bool result = await this.coachCategoryService.ExistsByIdAsync(existedCoachCategoryId);

            Assert.IsFalse(result);
        }

        // AllCoachCategoryNamesAsync Test
        [Test]
        public async Task AllCoachCategoryNamesAsyncShouldReturnCategoriesNames()
        {
            IEnumerable<string> actualAllNames = await this.dbContext
                .CoachCategories
                .Select(cc => cc.Name)
                .ToArrayAsync();

            IEnumerable<string> expectedAllNames = await this.coachCategoryService.AllCoachCategoryNamesAsync();

            Assert.IsTrue(actualAllNames.Count() == expectedAllNames.Count());
        }

        // GetDetailsByIdAsync Test
        [Test]
        public async Task GetDetailsByIdAsyncShouldReturnCorrectViewModel()
        {
            CoachCategoryDetailsViewModel actualViewModel = new CoachCategoryDetailsViewModel()
            {
                Id = CoachCategory.Id,
                Name = CoachCategory.Name,
                Coaches = this.dbContext.Coaches.Count(c => c.CoachCategoryId == CoachCategory.Id)
            };

            CoachCategoryDetailsViewModel expectedViewModel = await coachCategoryService.GetDetailsByIdAsync(CoachCategory.Id);

            Assert.IsTrue(expectedViewModel.Id == actualViewModel.Id && expectedViewModel.Name == actualViewModel.Name && expectedViewModel.Coaches == actualViewModel.Coaches);
        }
    }
}