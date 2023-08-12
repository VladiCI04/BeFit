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
            this.dbContext = new BeFitDbContext(this.dbOptions, false);

            this.dbContext.Database.EnsureCreated();
            SeedDtabase(this.dbContext);

            this.coachService = new CoachService(this.dbContext);
        }

        // CoachExistsByUserIdAsync Test
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

        // CoachExistsByPhoneNumberAsync Test
        [Test] 
        public async Task CoachExistsByPhoneNumberAsyncShouldReturnTrueWhenExists()
        {
            string existingCoachPhoneNumber = Coach.PhoneNumber.ToString();

            bool result = await this.coachService.CoachExistsByPhoneNumberAsync(existingCoachPhoneNumber);

            Assert.IsTrue(result);
        }
        [Test]
        public async Task CoachExistsByPhoneNumberAsyncShouldReturnFalseWhenNotExists()
        {
            string existingCoachUserPhoneNumber = CoachUser.PhoneNumber.ToString();

            bool result = await this.coachService.CoachExistsByPhoneNumberAsync(existingCoachUserPhoneNumber);

            Assert.IsFalse(result);
        }

        // HasEventsByUserIdAsync Test
        [Test]
        public async Task HasEventsByUserIdAsyncShouldReturnFalseWhenNotExists()
        {
            string existingUser = ClientUser.Id.ToString();

            bool result = await this.coachService.HasEventsByUserIdAsync(existingUser);

            Assert.IsFalse(result);
        }

        // GetCoachIdByUserIdAsync Test
        [Test]
        public async Task GetCoachIdByUserIdAsyncShouldReturnNotEmptyStringWhenIsTrue()
        {
            string existingCoachUserId = CoachUser.Id.ToString();

            string actualCoachId = Coach.Id.ToString();
            string expectingCoachId = await this.coachService.GetCoachIdByUserIdAsync(existingCoachUserId);

            Assert.IsTrue(expectingCoachId.Equals(actualCoachId));
        }
        [Test]
        public async Task GetCoachIdByUserIdAsyncShouldReturnNullWhenIsFalse()
        {
            string existingCoachUserId = ClientUser.Id.ToString();

            string actualCoachId = null!;
            string expectedCoachId = await this.coachService.GetCoachIdByUserIdAsync(existingCoachUserId);

            Assert.IsTrue(expectedCoachId == actualCoachId);
        }

        // GetCoachIdByCoachEmailAsync Test
        [Test]
        public async Task GetCoachIdByCoachEmailAsyncShouldReturnUserIdWhenIsTrue()
        {
            string coachEmail = Coach.User.Email;

            string actualCoachId = Coach.UserId.ToString();
            string expectedCoachId = await this.coachService.GetCoachIdByCoachEmailAsync(coachEmail);

            Assert.IsTrue(expectedCoachId.Equals(actualCoachId));
        }

        // HasEventWithIdAsync Test
        [Test]
        public async Task HasEventWithIdAsyncShouldReturnTrueWhenCoachExists()
        {
            string userId = Coach.UserId.ToString();
            string eventId = Even.Id.ToString();

            bool result = await this.coachService.HasEventWithIdAsync(userId, eventId);

            Assert.IsTrue(result);
        }
        [Test]
        public async Task HasEventWithIdAsyncShouldReturnFalseWhenCoachNotExists()
        {
            string userId = ClientUser.Id.ToString();
            string eventId = Even.Id.ToString();

            bool result = await this.coachService.HasEventWithIdAsync(userId, eventId);

            Assert.IsFalse(result);
        }
        [Test]
        public async Task HasEventWithIdAsyncShouldReturnFalseWhenEventNotExists()
        {
            string userId = ClientUser.Id.ToString();
            string eventId = Even2.Id.ToString();

            bool result = await this.coachService.HasEventWithIdAsync(userId, eventId);

            Assert.IsFalse(result);
        }

        // HasUserThisEvent Test
        [Test]
        public async Task HasUserThisEventShouldReturnTrueWhenUserHasTheEvent()
        {
            string userId = ClientUser.Id.ToString();
            string eventId = Even.Id.ToString();

            bool result = await this.coachService.HasUserThisEvent(userId, eventId);

            Assert.IsTrue(result);
        }
        [Test]
        public async Task HasUserThisEventShouldReturnFalseWhenUserHasNotTheEvent()
        {
            string userId = ClientUser2.Id.ToString();
            string eventId = Even.Id.ToString();

            bool result = await this.coachService.HasUserThisEvent(userId, eventId);

            Assert.IsFalse(result);
        }
    }
}