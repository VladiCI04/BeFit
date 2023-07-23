using BeFit.Data;
using BeFit.Data.Models;
using BeFit.Services.Data.Interfaces;
using BeFit.Web.ViewModels.Event;
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

		public async Task CreateAsync(EventFormModel formModel, string coachId)
		{
            Event newEvent = new Event()
            {
                Title = formModel.Title,
                Address = formModel.Address,
                Description = formModel.Description,
                ImageUrl = formModel.ImageUrl,
                Tax = formModel.Tax,
                CreatedOn = formModel.CreatedOn,
                Start = formModel.Start,
                End = formModel.End,
                CoachId = Guid.Parse(coachId),
                EventCategoryId = formModel.EventCategoryId,
                Clients = formModel.Clients
            };

            await this.dbContext.Events.AddAsync(newEvent);
            await this.dbContext.SaveChangesAsync();
		}
	}
}
