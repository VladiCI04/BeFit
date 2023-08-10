using BeFit.Data;
using Microsoft.EntityFrameworkCore;
using static BeFit.Services.Tests.DatabaseSeeder;

namespace BeFit.Services.Tests
{
    public class CoachServiceTests
    {
        private DbContextOptions<BeFitDbContext> dbOptions;
        private BeFitDbContext dbContext;

        [OneTimeSetUp]
        public void OnTimeSetUp() 
        {
            this.dbOptions = new DbContextOptionsBuilder<BeFitDbContext>()
                .UseInMemoryDatabase("BeFitInMemory" + Guid.NewGuid().ToString())
                .Options;
            this.dbContext = new BeFitDbContext(this.dbOptions);

            this.dbContext.Database.EnsureCreated();
            SeedDtabase(this.dbContext);
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CoachExistsByUserIdAsyncShouldReturnTrueWhenExists()
        {
            Assert.Pass();
        }
    }
}