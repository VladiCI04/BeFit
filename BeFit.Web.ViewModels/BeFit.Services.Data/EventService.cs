using BeFit.Data;
using BeFit.Data.Models;
using BeFit.Services.Data.Interfaces;
using BeFit.Services.Data.Models.Events;
using BeFit.Services.Data.Models.Statistics;
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

		public async Task<string> CreateAndReturnIdAsync(EventFormModel formModel, string coachId)
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
                EventCategoryId = formModel.EventCategoryId
            };

            await this.dbContext.Events.AddAsync(newEvent);
            await this.dbContext.SaveChangesAsync();

            return newEvent.Id.ToString();
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
					.OrderByDescending(e => e.CreatedOn),
				EventSorting.Oldest => eventsQuery
					.OrderBy(e => e.CreatedOn),
				EventSorting.TaxAscending => eventsQuery
					.OrderBy(e => e.Tax),
				EventSorting.TaxDescending => eventsQuery
					.OrderByDescending(e => e.Tax),
				_ => throw new NotImplementedException()
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
                    CoachName = $"{e.Coach.User.FirstName} {e.Coach.User.LastName}"
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
                    CoachName = $"{e.Coach.User.FirstName} {e.Coach.User.LastName}"
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
					CoachName = $"{e.Event.Coach.User.FirstName} {e.Event.Coach.User.LastName}",
				})
				.ToArrayAsync();

			return allUsersEvents;
		}

		public async Task<EventDetailsViewModel?> GetDetailsByIdAsync(string eventId)
		{
            Event? even = await this.dbContext
                .Events
                .Include(e => e.EventCategory)
                .Include(e => e.Coach)
                .ThenInclude(e => e.User)
                .Where(e => e.IsActive)
                .FirstAsync(e => e.Id.ToString() == eventId);

            return new EventDetailsViewModel
            {
                Id = even.Id.ToString(),
                Title = even.Title,
                Address = even.Address,
                ImageUrl = even.ImageUrl,
                Tax = even.Tax,
                Description = even.Description,
                Category = even.EventCategory.Name,
                Start = even.Start,
                End = even.End,
                Clients = this.dbContext.EventClients.ToList().Count(ec => ec.EventId == even.Id),
                Coach = new Web.ViewModels.Coach.CoachInfoOnEventViewModel()
                {
                    FullName = $"{even.Coach.User.FirstName} {even.Coach.User.LastName}",
                    Email = even.Coach.User.Email,
                    PhoneNumber = even.Coach.PhoneNumber,
                    Category = even.Coach.CoachCategoryId
                }
            };
		}

        public async Task<bool> ExistsByIdAsync(string eventId)
        {
            bool result = await this.dbContext
                 .Events
                 .Where(e => e.IsActive)
                 .AnyAsync(e => e.Id.ToString() == eventId);

            return result;
        }

        public async Task<EventFormModel> GetEventForEditByIdAsync(string eventId)
        {
            Event? even = await this.dbContext
                .Events
                .Include(e => e.EventCategory)
                .Where(e => e.IsActive)
                .FirstAsync(e => e.Id.ToString() == eventId);

            return new EventFormModel
            {
                Title = even.Title,
                Address = even.Address,
                Description = even.Description,
                ImageUrl = even.ImageUrl,
                Tax = even.Tax,
                Start = even.Start,
                End = even.End,
                EventCategoryId = even.EventCategoryId
            };
        }

        public async Task<bool> IsCoachWithIdOwnerOfEventWithIdAsync(string eventId, string coachId)
        {
            Event even = await this.dbContext
                .Events
                .Where(e => e.IsActive)
                .FirstAsync(e => e.Id.ToString() == eventId);

            return even.CoachId.ToString() == coachId;
        }

		public async Task EditEventByIdAndFormModelAsync(string eventId, EventFormModel formModel)
		{
			Event even = await this.dbContext
                .Events
                .Where(e => e.IsActive)
                .FirstAsync(e => e.Id.ToString() == eventId);

            even.Title = formModel.Title;
            even.Address = formModel.Address;
            even.Description = formModel.Description;
            even.ImageUrl = formModel.ImageUrl;
            even.Tax = formModel.Tax;
            even.EventCategoryId = formModel.EventCategoryId;
            even.Start = formModel.Start;   
            even.End = formModel.End;

            await this.dbContext.SaveChangesAsync();
		}

		public async Task<EventPreDeleteDetailsViewModel> GetEventForDeleteByIdAsync(string eventId)
		{
            Event even = await this.dbContext
                .Events
                .Where(e => e.IsActive)
                .FirstAsync(e => e.Id.ToString() == eventId);

            return new EventPreDeleteDetailsViewModel
            {
                Title = even.Title,
                Address = even.Address,
                ImageUrl = even.ImageUrl
            };
		}

		public async Task DeleteEventByIdAsync(string eventId)
		{
            Event eventToDelete = await this.dbContext
                .Events
                .Where(e => e.IsActive)
                .FirstAsync(e => e.Id.ToString() == eventId);

            eventToDelete.IsActive = false;

            await this.dbContext.SaveChangesAsync();
		}

		public async Task<bool> AddEventToMineAsync(string userId, EventDetailsViewModel even)
		{
			bool alreadyAdded = await dbContext
                .EventClients
				.AnyAsync(ec => ec.ClientId.ToString() == userId && ec.EventId.ToString() == even.Id);

			if (!alreadyAdded)
			{
				EventClient userEvent = new EventClient
				{
					ClientId = Guid.Parse(userId),
					EventId = Guid.Parse(even.Id)
				};

                even.Clients += 1;

				await dbContext.EventClients.AddAsync(userEvent);
				await dbContext.SaveChangesAsync();

                return true;
			}
            else
            {
                even.Clients -= 1;
                return false;
            }
		}

		public async Task<EventDetailsViewModel?> GetEventDetailsByIdAsync(string id)
		{
		    return await dbContext
               .Events
			   .Where(e => e.Id.ToString() == id)
			   .Select(e => new EventDetailsViewModel
			   {
				   Id = e.Id.ToString(),
                   Title = e.Title,
                   Address = e.Address,
                   ImageUrl = e.ImageUrl,
                   Tax = e.Tax, 
                   CoachName = $"{e.Coach.User.FirstName} {e.Coach.User.LastName}"
               })
			   .FirstOrDefaultAsync();
		}

		public async Task<Event> GetEventByIdAsync(string id)
		{
			Event even = await dbContext
			   .Events
			   .Where(e => e.Id.ToString() == id)
			   .FirstAsync();

            return even;
		}

		public async Task RemoveEventFromMineAsync(string userId, EventDetailsViewModel even)
		{
			var userEvent = await dbContext.EventClients
				.FirstOrDefaultAsync(ec => ec.ClientId.ToString() == userId && ec.EventId.ToString() == even.Id);

			if (userEvent != null)
			{
				dbContext.EventClients.Remove(userEvent);
				await dbContext.SaveChangesAsync();
			}
		}

		public async Task<StatisticsServiceModel> GetStatisticsAsync()
		{
            return new StatisticsServiceModel()
            {
                TotalEvents = await this.dbContext.Events.CountAsync(),
                TotalClients = await this.dbContext.Users.CountAsync(),
                TotalCoaches = await this.dbContext.Coaches.CountAsync(),
            };
		}

        public async Task<bool> IsEventExpired(string eventId)
        {
            eventId = eventId.ToLower();
            Event? even = await this.dbContext
                .Events
                .FirstAsync(e => e.Id.ToString() == eventId);

            if (even.End >= DateTime.Now)
            {
                return true;
            }

            even.IsActive = false;
            await this.dbContext.SaveChangesAsync();

            return false;
        }
	}
}
