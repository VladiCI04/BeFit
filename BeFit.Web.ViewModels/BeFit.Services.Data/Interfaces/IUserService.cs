namespace BeFit.Services.Data.Interfaces
{
    public interface IUserService
    {
        Task<string?> GetFullNameByEmailAsync(string email);
    }
}
