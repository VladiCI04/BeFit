using BeFit.Data;
using BeFit.Data.Models;
using BeFit.Services.Data.Interfaces;
using BeFit.Services.Data.Models.Events;
using BeFit.Web.ViewModels.Event;
using BeFit.Web.ViewModels.Event.Enums;
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

		public async Task<AllEventsFilteredAndPagedServiceModel> AllAsync(AllEventsQueryModel queryModel)
		{
            IQueryable<Event> eventsQuery = this.dbContext
                .Events
                .AsQueryable();

			if (!string.IsNullOrWhiteSpace(queryModel.EventCategory)) 
            {
                eventsQuery = eventsQuery
                    .Where(e => e.EventCategory.Name == queryModel.EventCategory);
            }

			if (!string.IsNullOrEmpty(queryModel.SearchString))
            {
                string wildCard = $"%{queryModel.SearchString.ToLower()}%";
                eventsQuery = eventsQuery
                    .Where(e => EF.Functions.Like(e.Title, wildCard) ||
                                EF.Functions.Like(e.Address, wildCard) ||
                                EF.Functions.Like(e.Description, wildCard));
            }

            eventsQuery = queryModel.EventSorting switch
            {
                EventSorting.Newest => eventsQuery
                    .OrderBy(e => e.CreatedOn),
                EventSorting.Oldest => eventsQuery
                    .OrderByDescending(e => e.CreatedOn),
                EventSorting.TaxAscending => eventsQuery
                    .OrderBy(e => e.Tax),
                EventSorting.TaxDescending => eventsQuery
                    .OrderByDescending(e => e.Tax)
            };

            IEnumerable<EventAllViewModel> allEvents = await eventsQuery
                .Skip((queryModel.CurrentPage - 1) * queryModel.EventsPerPage)
                .Take(queryModel.EventsPerPage)
                .Select(e => new EventAllViewModel()
                {
                    Id = e.Id.ToString(),
                    Title = e.Title,
                    Address = e.Address,
                    ImageUrl = e.ImageUrl,
                    Tax = e.Tax,
                    CoachName = e.Coach.Name
                })
                .ToArrayAsync();

			int totalEvents = eventsQuery.Count();

            return new AllEventsFilteredAndPagedServiceModel()
            {
                TotalEventsCount = totalEvents,
                Events = allEvents
            };
		}
	}
}
