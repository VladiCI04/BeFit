using BeFit.Data;
using BeFit.Services.Data.Interfaces;
using BeFit.Services.Data.Models.Events;
using BeFit.Web.ViewModels.Event;
using BeFit.Web.ViewModels.Event.Enums;
using BeFit.Web.ViewModels.Home;
using Microsoft.EntityFrameworkCore;
using static BeFit.Common.EntityValidationConstants;

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
                .Where(e => e.IsActive)
                .OrderByDescending(e => e.CreatedOn)
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
            BeFit.Data.Models.Event newEvent = new BeFit.Data.Models.Event()
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
            IQueryable<BeFit.Data.Models.Event> eventsQuery = this.dbContext
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
                    .OrderByDescending(e => e.CreatedOn),
                EventSorting.Oldest => eventsQuery
                    .OrderBy(e => e.CreatedOn),
                EventSorting.TaxAscending => eventsQuery
                    .OrderBy(e => e.Tax),
                EventSorting.TaxDescending => eventsQuery
                    .OrderByDescending(e => e.Tax)
            };

            IEnumerable<EventAllViewModel> allEvents = await eventsQuery
                .Where(e => e.IsActive)
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

		public async Task<IEnumerable<EventAllViewModel>> AllByCoachIdAsync(string coachId)
		{
            IEnumerable<EventAllViewModel> allCoachEvents = await this.dbContext
                .Events
                .Where(e => e.IsActive && e.CoachId.ToString() == coachId)
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
                   
            return allCoachEvents;
		}

		public async Task<IEnumerable<EventAllViewModel>> AllByUserIdAsync(string userId)
		{
			IEnumerable<EventAllViewModel> allUsersEvents = await this.dbContext
				.EventClients
				.Where(ec => ec.Event.IsActive && ec.ClientId.ToString() == userId)
				.Select(e => new EventAllViewModel()
				{
					Id = e.Event.Id.ToString(),
					Title = e.Event.Title,
					Address = e.Event.Address,
					ImageUrl = e.Event.ImageUrl,
					Tax = e.Event.Tax,
					CoachName = e.Event.Coach.Name,
				})
				.ToArrayAsync();

			return allUsersEvents;
		}
	}
}
