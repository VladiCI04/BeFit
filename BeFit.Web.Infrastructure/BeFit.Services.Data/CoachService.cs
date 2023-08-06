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
				Age = model.Age,
				Gender = model.Gender,
				Height = model.Height,
				Weight = model.Weight,
				PhoneNumber = model.PhoneNumber,
				Description = model.Description,
				CoachCategoryId = model.CoachCategoryId
			};

			await this.dbContext.Coaches.AddAsync(newCoach);
			await this.dbContext.SaveChangesAsync();
		}

		public async Task<string> GetCoachIdByUserIdAsync(string userId)
		{
			Coach? coach = await this.dbContext
				.Coaches
				.FirstOrDefaultAsync(c => c.UserId.ToString() == userId);

			if (coach == null)
			{
				return null!;
			}
			else
			{
				return coach.Id.ToString();
			}
		}

		public async Task<bool> HasEventWithIdAsync(string userId, string eventId)
		{
			Coach? coach = await this.dbContext
				.Coaches
				.Include(c => c.Events)
				.FirstOrDefaultAsync(c => c.UserId.ToString() == userId);
			if (coach == null)
			{
				return false;
			}

			eventId = eventId.ToLower();
			bool result = coach.Events.Any(e => e.Id.ToString() == eventId);

			return result;
		}

		public async Task<bool> HasUserThisEvent(string userId, string eventId)
		{
			eventId = eventId.ToLower();
			bool result = await dbContext
				.EventClients
				.AnyAsync(ec => ec.ClientId.ToString() == userId && ec.EventId.ToString() == eventId);

			return result;
		}
	}
}
