using BeFit.Data;
using BeFit.Services.Data;
using BeFit.Services.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using static BeFit.Services.Tests.DatabaseSeeder;

namespace BeFit.Services.Tests
{
    public class CoachServiceTests
    {
        private DbContextOptions<BeFitDbContext> dbOptions;
        private BeFitDbContext dbContext;
        private ICoachService coachService;

        [OneTimeSetUp]
        public void OnTimeSetUp() 
        {
            this.dbOptions = new DbContextOptionsBuilder<BeFitDbContext>()
                .UseInMemoryDatabase("BeFitInMemory" + Guid.NewGuid().ToString())
                .Options;
            this.dbContext = new BeFitDbContext(this.dbOptions);

            this.dbContext.Database.EnsureCreated();
            SeedDtabase(this.dbContext);

            this.coachService = new CoachService(this.dbContext);
        }

        [Test]
        public async Task CoachExistsByUserIdAsyncShouldReturnTrueWhenExists()
        {
            string existingCoachUserId = CoachUser.Id.ToString();

            bool result = await this.coachService.CoachExistsByUserIdAsync(existingCoachUserId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task CoachExistsByUserIdAsyncShouldReturnFalseWhenNotExists()
        {
            string existingCoachUserId = ClientUser.Id.ToString();

            bool result = await this.coachService.CoachExistsByUserIdAsync(existingCoachUserId);

            Assert.IsFalse(result);
        }
    }
}