using BeFit.Data;
using BeFit.Data.Models;
using BeFit.Services.Data.Interfaces;
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
    }
}
