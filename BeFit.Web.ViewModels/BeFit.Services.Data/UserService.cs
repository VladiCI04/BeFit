using BeFit.Data;
using BeFit.Data.Models;
using BeFit.Services.Data.Interfaces;
using BeFit.Web.ViewModels.User;
using Microsoft.EntityFrameworkCore;

namespace BeFit.Services.Data
{
	public class UserService : IUserService
    {
        private readonly BeFitDbContext dbContext;

        public UserService(BeFitDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

		public async Task<string?> GetFullNameByEmailAsync(string email)
        {
            ApplicationUser? user = await this.dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return string.Empty;
            }

            return $"{user.FirstName} {user.LastName}";
        }

		public async Task<string> GetFullNameByIdAsync(string userId)
		{
            ApplicationUser? user = await this.dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Id.ToString() == userId);

            if (user == null)
            {
                return string.Empty;
            }

            return $"{user.FirstName} {user.LastName}";
		}

		public async Task<IEnumerable<UserViewModel>> AllAsync()
		{
            List<UserViewModel> allUsers = await this.dbContext
                .Users
                .Select(u => new UserViewModel 
                { 
                    Id = u.Id.ToString(),
                    FullName = $"{u.FirstName} {u.LastName}",
                    Email = u.Email
				})
                .ToListAsync();

            foreach (UserViewModel user in allUsers)
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

            return allUsers;
		}
	}
}
