using BeFit.Data;
using BeFit.Services.Data.Interfaces;
using BeFit.Web.ViewModels.Home;
using Microsoft.EntityFrameworkCore;

namespace BeFit.Services.Data
{
    public class EventService : IEventService
    {
        private readonly BeFitDbContext dbContext;

        public EventService(BeFitDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<IndexViewModel>> AllEventsAsync()
        {
            IEnumerable<IndexViewModel> allEvents = await this.dbContext
                .Events
                .Select(e => new IndexViewModel()
                {
                    Id = e.Id.ToString(),
                    Title = e.Title,
                    ImageUrl = e.ImageUrl
                })
                .ToArrayAsync();

            return allEvents;
        }
    }
}
