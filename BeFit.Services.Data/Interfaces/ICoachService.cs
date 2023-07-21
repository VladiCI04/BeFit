using BeFit.Web.ViewModels.Coach;

namespace BeFit.Services.Data.Interfaces
{
    public interface ICoachService
    {
        Task<bool> CoachExistsByUserIdAsync(string userId);

        Task<bool> CoachExistsByPhoneNumberAsync(string phoneNumber);

        Task<bool> HasEventsByUserIdAsync(string userId);

        Task Create(string userId, BecomeCoachFormModel model);
    }
}
