using BeFit.Web.ViewModels.Home;

namespace BeFit.Services.Data.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<IndexViewModel>> AllEventsAsync();
    }
}
