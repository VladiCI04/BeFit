using BeFit.Data;
using BeFit.Data.Models;
using BeFit.Services.Data.Interfaces;
using BeFit.Web.ViewModels.Coach;
using Microsoft.EntityFrameworkCore;

namespace BeFit.Services.Data
{
    public class CoachService : ICoachService
    {
        private readonly BeFitDbContext dbContext;

        public CoachService(BeFitDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

		public async Task<bool> CoachExistsByUserIdAsync(string userId)
        {
            bool result = await this.dbContext
                .Coaches
                .AnyAsync(c => c.UserId.ToString() == userId);

            return result;
        }

		public async Task<bool> CoachExistsByPhoneNumberAsync(string phoneNumber)
		{
			bool result = await this.dbContext
				.Coaches
				.AnyAsync(c => c.PhoneNumber == phoneNumber);

			return result;
		}

		public async Task<bool> HasEventsByUserIdAsync(string userId)
		{
			ApplicationUser? user = await this.dbContext
				.Users
				.FirstOrDefaultAsync(u => u.Id.ToString() == userId);

			if (user == null)
			{
				return false;
			}

			return user.Events.Any();
		}

		public async Task Create(string userId, BecomeCoachFormModel model)
		{
			Coach newCoach = new Coach()
			{
				UserId = Guid.Parse(userId),
				Name = model.Name,
				Age = model.Age,
				Gender = model.Gender,
				Height = model.Height,
				Weight = model.Weight,
				PhoneNumber = model.PhoneNumber,
				Email = model.Email,
				Description = model.Description
			};

			await this.dbContext.Coaches.AddAsync(newCoach);
			await this.dbContext.SaveChangesAsync();
		}
	}
}
