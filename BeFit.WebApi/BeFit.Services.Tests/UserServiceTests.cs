using BeFit.Data;
using BeFit.Data.Models;
using BeFit.Services.Data;
using BeFit.Services.Data.Interfaces;
using BeFit.Web.ViewModels.User;
using Microsoft.EntityFrameworkCore;
using static BeFit.Services.Tests.DatabaseSeeder;

namespace BeFit.Services.Tests
{
    public class UserServiceTests
    {
        private DbContextOptions<BeFitDbContext> dbOptions;
        private BeFitDbContext dbContext;
        private IUserService userService;

        [OneTimeSetUp]
        public void OnTimeSetUp() 
        {
            this.dbOptions = new DbContextOptionsBuilder<BeFitDbContext>()
                .UseInMemoryDatabase("BeFitInMemory" + Guid.NewGuid().ToString())
                .Options;
            this.dbContext = new BeFitDbContext(this.dbOptions, false);

            this.dbContext.Database.EnsureCreated();
            SeedDtabase(this.dbContext);

            this.userService = new UserService(this.dbContext);
        }

        // GetFullNameByEmailAsync Test
        [Test]
        public async Task GetFullNameByEmailAsyncShouldReturnUserFullNameIfEmailExists()
        {
            string existUserEmail = ClientUser.Email;

            string actualUserFullName = $"{ClientUser.FirstName} {ClientUser.LastName}";

            string? expectedUserFullName = await userService.GetFullNameByEmailAsync(existUserEmail);

            Assert.IsTrue(expectedUserFullName?.Equals(actualUserFullName));
        }
        [Test]
        public async Task GetFullNameByEmailAsyncShouldReturnEmptyStringIfEmailNotExists()
        {
            string existUserEmail = "gosho@abv.bg";

            string actualUserFullName = string.Empty;

            string? expectedUserFullName = await userService.GetFullNameByEmailAsync(existUserEmail);

            Assert.IsTrue(expectedUserFullName?.Equals(actualUserFullName));
        }

        // GetFullNameByIdAsync Test
        [Test]
        public async Task GetFullNameByIdAsyncShouldReturnUserFullNameIfIdExists()
        {
            string existUserId = ClientUser.Id.ToString();

            string actualUserFullName = $"{ClientUser.FirstName} {ClientUser.LastName}";

            string? expectedUserFullName = await userService.GetFullNameByIdAsync(existUserId);

            Assert.IsTrue(expectedUserFullName?.Equals(actualUserFullName));
        }
        [Test]
        public async Task GetFullNameByIdAsyncShouldReturnEmptyStringIfIdNotExists()
        {
            string existUserId = "018fc1c5-f5ae-47f2-a15a-822a1ed19bed";

            string actualUserFullName = string.Empty;

            string? expectedUserFullName = await userService.GetFullNameByIdAsync(existUserId);

            Assert.IsTrue(expectedUserFullName?.Equals(actualUserFullName));
        }

        // AllAsync Test
        [Test]
        public async Task AllAsync()
        {
            List<UserViewModel> actualAllUsers = await this.dbContext
                .Users
                .Select(u => new UserViewModel
                {
                    Id = u.Id.ToString(),
                    FullName = $"{u.FirstName} {u.LastName}",
                    Email = u.Email
                })
                .ToListAsync();

            foreach (UserViewModel user in actualAllUsers)
            {
                Coach? coach = this.dbContext
                    .Coaches
                    .FirstOrDefault(c => c.UserId.ToString() == user.Id);

                if (coach != null)
                {
                    user.PhoneNumber = coach.PhoneNumber;
                }
                else
                {
                    user.PhoneNumber = string.Empty;
                }
            }

            IEnumerable<UserViewModel> expectedAllUsers = await this.userService.AllAsync();

            Assert.IsTrue(actualAllUsers.Count() == expectedAllUsers.Count());
        }
    }
}