using BeFit.Data;
using BeFit.Services.Data;
using BeFit.Services.Data.Interfaces;
using BeFit.Services.Data.Models.Events;
using BeFit.Web.ViewModels.Coach;
using BeFit.Web.ViewModels.Event;
using BeFit.Web.ViewModels.Home;
using Microsoft.EntityFrameworkCore;
using static BeFit.Services.Tests.DatabaseSeeder;

namespace BeFit.Services.Tests
{
    public class EventServiceTests
    {
        private DbContextOptions<BeFitDbContext> dbOptions;
        private BeFitDbContext dbContext;
        private IEventService eventService;

        [OneTimeSetUp]
        public void OnTimeSetUp() 
        {
            this.dbOptions = new DbContextOptionsBuilder<BeFitDbContext>()
                .UseInMemoryDatabase("BeFitInMemory" + Guid.NewGuid().ToString())
                .Options;
            this.dbContext = new BeFitDbContext(this.dbOptions, false);

            this.dbContext.Database.EnsureCreated();
            SeedDtabase(this.dbContext);

            this.eventService = new EventService(this.dbContext);
        }

        // AllEventsAsync Test
        [Test]
        public async Task AllEventsAsyncShouldReturnAllEvents()
        {
            IEnumerable<IndexViewModel> actualAllEvents = await this.dbContext
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

            IEnumerable<IndexViewModel> expectedAllEvents = await this.eventService.AllEventsAsync();

            Assert.IsTrue(actualAllEvents.Count() == expectedAllEvents.Count());
        }

        // CreateAndReturnIdAsync Test 
        [Test]
        public async Task CreateAndReturnIdAsyncShouldReturnNewEventId()
        {
            string existCoachId = Coach.Id.ToString();

            EventFormModel formModel = new EventFormModel()
            {
                Title = "EventTest1",
                Address = "Storgosia90",
                Description = "Best best test event ever!",
                ImageUrl = "https://www.adobe.com/content/dam/www/us/en/events/overview-page/eventshub_evergreen_opengraph_1200x630_2x.jpg",
                Tax = 12,
                CreatedOn = DateTime.Parse("2023-08-08 13:26:05.0633333"),
                Start = DateTime.Parse("2023-08-25 13:24:00.0000000"),
                End = DateTime.Parse("2023-08-25 13:24:00.0000000"),
                EventCategoryId = EventCategory.Id
            };

            string expectedId = await this.eventService.CreateAndReturnIdAsync(formModel, existCoachId);
            string actualId = this.dbContext.Events.First(e => e.Title == formModel.Title).Id.ToString();

            Assert.IsTrue(expectedId.Equals(actualId));
        }

        // AllAsync Test
        [Test]
        public async Task AllAsyncShouldReturnCorrectAllEventsFilteredAndPagedServiceModel()
        {
            List<EventAllViewModel> eventList = new List<EventAllViewModel>();
            EventAllViewModel viewModel = new EventAllViewModel()
            {
                Id = Even.Id.ToString(),
                Title = Even.Title,
                Address = Even.Address,
                ImageUrl = Even.ImageUrl,
                Tax = Even.Tax,
                CoachName = $"{Even.Coach.User.FirstName} {Even.Coach.User.LastName}"
            };
            eventList.Add(viewModel);

            List<string> eventCategoriesList = new List<string>();
            string eventCategoryName = EventCategory.Name;
            eventCategoriesList.Add(eventCategoryName);

            AllEventsQueryModel allEventsQueryModel = new AllEventsQueryModel()
            {
                Events = eventList,
                TotalEvents = 1,
                EventCategories = eventCategoriesList
            };
            AllEventsFilteredAndPagedServiceModel allEventsFilteredAndPagedServiceModel = await this.eventService.AllAsync(allEventsQueryModel);

            Assert.IsFalse(allEventsFilteredAndPagedServiceModel.Events.Count() == 1 && allEventsFilteredAndPagedServiceModel.TotalEventsCount == 1);
        }

        // AllByCoachIdAsync Test
        [Test]
        public async Task AllByCoachIdAsyncShouldReturnAllEvents()
        {
            string existsCoachId = Coach.Id.ToString(); 

            IEnumerable<EventAllViewModel> actualAllCoachEvents = await this.dbContext
                .Events
                .Where(e => e.IsActive && e.CoachId.ToString() == existsCoachId)
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

            IEnumerable<EventAllViewModel> expectedAllCoachEvents = await this.eventService.AllByCoachIdAsync(existsCoachId);

            Assert.IsTrue(actualAllCoachEvents.Count() == expectedAllCoachEvents.Count());
        }
        [Test]
        public async Task AllByCoachIdAsyncShouldReturnZero()
        {
            string existsCoachId = ClientUser.Id.ToString();

            IEnumerable<EventAllViewModel> actualAllCoachEvents = await this.dbContext
                .Events
                .Where(e => e.IsActive && e.CoachId.ToString() == existsCoachId)
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

            IEnumerable<EventAllViewModel> expectedAllCoachEvents = await this.eventService.AllByCoachIdAsync(existsCoachId);

            Assert.IsTrue(actualAllCoachEvents.Count() == expectedAllCoachEvents.Count());
        }

        // AllByUserIdAsync Test
        [Test]
        public async Task AllByUserIdAsyncShouldReturnAllEventsOfThisUser()
        {
            string existUserId = ClientUser.Id.ToString();

            IEnumerable<EventAllViewModel> actualUsersEvents = await this.dbContext
            .EventClients
                .Where(ec => ec.Event.IsActive && ec.ClientId.ToString() == existUserId)
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

            IEnumerable<EventAllViewModel> expectedUsersEvents = await this.eventService.AllByUserIdAsync(existUserId); 

            Assert.IsTrue(actualUsersEvents.Count() == expectedUsersEvents.Count()); 
        }
        [Test]
        public async Task AllByUserIdAsyncShouldReturnFalse()
        {
            string existUserId = ClientUser2.Id.ToString();

            IEnumerable<EventAllViewModel> actualUsersEvents = await this.dbContext
            .EventClients
                .Where(ec => ec.Event.IsActive && ec.ClientId.ToString() == existUserId)
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

            IEnumerable<EventAllViewModel> expectedUsersEvents = await this.eventService.AllByUserIdAsync(existUserId);

            Assert.IsTrue(actualUsersEvents.Count() == expectedUsersEvents.Count());
        }

        // GetDetailsByIdAsync Test
        [Test]
        public async Task GetDetailsByIdAsyncShouldReturnCorrectEventDetailsViewModel()
        {
            string existEventId = Even.Id.ToString();

            EventDetailsViewModel actualEventDetailsViewModel = new EventDetailsViewModel()
            {
                Id = Even.Id.ToString(),
                Title = Even.Title,
                Address = Even.Address,
                ImageUrl = Even.ImageUrl,
                Tax = Even.Tax,
                Description = Even.Description,
                Category = Even.EventCategory.Name,
                Start = Even.Start,
                End = Even.End,
                Clients = this.dbContext.EventClients.Count(ec => ec.EventId == Even.Id),
                Coach = new Web.ViewModels.Coach.CoachInfoOnEventViewModel()
                {
                    FullName = $"{Even.Coach.User.FirstName} {Even.Coach.User.LastName}",
                    Email = Even.Coach.User.Email,
                    PhoneNumber = Even.Coach.PhoneNumber,
                    Category = Even.Coach.CoachCategoryId
                }
            };

            EventDetailsViewModel? expectedEventDetailsViewModel = await this.eventService.GetDetailsByIdAsync(existEventId);

            Assert.IsTrue(actualEventDetailsViewModel.Id == expectedEventDetailsViewModel?.Id &&
                    actualEventDetailsViewModel.Tax == expectedEventDetailsViewModel?.Tax &&
                    actualEventDetailsViewModel.End == expectedEventDetailsViewModel?.End &&
                    actualEventDetailsViewModel.Coach.Email == expectedEventDetailsViewModel?.Coach.Email &&
                    actualEventDetailsViewModel.CoachName == expectedEventDetailsViewModel?.CoachName &&
                    actualEventDetailsViewModel.Address == expectedEventDetailsViewModel?.Address &&
                    actualEventDetailsViewModel.Category == expectedEventDetailsViewModel?.Category &&
                    actualEventDetailsViewModel.Description == expectedEventDetailsViewModel?.Description &&
                    actualEventDetailsViewModel.ImageUrl == expectedEventDetailsViewModel?.ImageUrl &&
                    actualEventDetailsViewModel.Start == expectedEventDetailsViewModel?.Start &&
                    actualEventDetailsViewModel.Title == expectedEventDetailsViewModel?.Title);
        }
        [Test]
        public void GetDetailsByIdAsyncShouldReturnNull()
        {
            string existEventId = "b9d7f272-60bf-48dc-ad73-c19f2072a435";          

            Assert.ThrowsAsync<InvalidOperationException>(async () => await this.eventService.GetDetailsByIdAsync(existEventId));
        }

        // ExistsByIdAsync
        [Test]
        public async Task ExistsByIdAsyncShouldReturnTrueIfExists()
        {
            string existEventId = Even.Id.ToString();

            bool result = await this.eventService.ExistsByIdAsync(existEventId);

            Assert.IsTrue(result);
        }
        [Test]
        public async Task ExistsByIdAsyncShouldReturnFalseIfNotExists()
        {
            string existEventId = "b9d7f272-60bf-48dc-ad73-c19f2072a435";

            bool result = await this.eventService.ExistsByIdAsync(existEventId);

            Assert.IsFalse(result);
        }

        // GetEventForEditByIdAsync
        [Test]
        public async Task GetEventForEditByIdAsyncShouldReturnCorrectEventFormModel()
        {
            string existEventId = Even.Id.ToString();
            EventFormModel actualEventForm = new EventFormModel()
            {
                Title = Even.Title,
                Address = Even.Address,
                Description = Even.Description,
                ImageUrl = Even.ImageUrl,
                Tax = Even.Tax,
                Start = Even.Start,
                End = Even.End, 
                EventCategoryId = Even.EventCategoryId
            };

            EventFormModel expectedEventForm = await this.eventService.GetEventForEditByIdAsync(existEventId);

            Assert.IsTrue(actualEventForm.EventCategoryId == expectedEventForm.EventCategoryId &&
                       actualEventForm.CreatedOn == expectedEventForm.CreatedOn &&
                       actualEventForm.End == expectedEventForm.End &&
                       actualEventForm.Tax == expectedEventForm.Tax &&
                       actualEventForm.Start == expectedEventForm.Start &&
                       actualEventForm.Address == expectedEventForm.Address);
        }
        [Test]
        public void GetEventForEditByIdAsyncShouldReturnException()
        {
            string existEventId = "b9d7f272-60bf-48dc-ad73-c19f2072a435";

            Assert.ThrowsAsync<InvalidOperationException>(async () => await this.eventService.GetEventForEditByIdAsync(existEventId));
        }

        // IsCoachWithIdOwnerOfEventWithIdAsync Test
        [Test]
        public async Task IsCoachWithIdOwnerOfEventWithIdAsyncShouldReturnTrueIfEventIdExist()
        {
            string existEventId = Even.Id.ToString();
            string existCoachId = Coach.Id.ToString();

            bool result = await this.eventService.IsCoachWithIdOwnerOfEventWithIdAsync(existEventId, existCoachId);

            Assert.IsTrue(result);
        }
        [Test]
        public void IsCoachWithIdOwnerOfEventWithIdAsyncShouldReturnFalseIfEventIdNotExist()
        {
            string existEventId = "8d7db5dc-c7f6-4e1e-bf6e-821d314f5b57";
            string existCoachId = Coach.Id.ToString();

            Assert.ThrowsAsync<InvalidOperationException>(async () => await this.eventService.IsCoachWithIdOwnerOfEventWithIdAsync(existEventId, existCoachId));
        }
        [Test]
        public async Task IsCoachWithIdOwnerOfEventWithIdAsyncShouldReturnFalseIfCoachIdNotExist()
        {
            string existEventId = Even.Id.ToString();
            string existCoachId = "8d7db5dc-c7f6-4e1e-bf6e-821d314f5b57";

            bool result = await this.eventService.IsCoachWithIdOwnerOfEventWithIdAsync(existEventId, existCoachId);

            Assert.IsFalse(result);
        }
        [Test]
        public void IsCoachWithIdOwnerOfEventWithIdAsyncShouldReturnFalseIfCoachIdAndEventIdNotExist()
        {
            string existEventId = "b45f9e97-7402-4370-890a-58379437a932";
            string existCoachId = "8d7db5dc-c7f6-4e1e-bf6e-821d314f5b57";

            Assert.ThrowsAsync<InvalidOperationException>(async () => await this.eventService.IsCoachWithIdOwnerOfEventWithIdAsync(existEventId, existCoachId));
        }

        // EditEventByIdAndFormModelAsync Test
        [Test]
        public void EditEventByIdAndFormModelAsyncShouldReturnException()
        {
            string eventId = "b45f9e97-7402-4370-890a-58379437a932";

            EventFormModel formModel = new EventFormModel()
            {
                Title = Even.Title,
                Address = Even.Address,
                Description = Even.Description,
                ImageUrl = Even.ImageUrl,
                Tax = Even.Tax,
                EventCategoryId = Even.EventCategoryId,
                Start = Even.Start,
                End = Even.End
            };

            Assert.ThrowsAsync<InvalidOperationException>(async () => await this.eventService.EditEventByIdAndFormModelAsync(eventId, formModel));
        }
        [Test]
        public async Task EditEventByIdAndFormModelAsyncShouldReturnCorrectEventFormModel()
        {
            string eventId = Even.Id.ToString();

            EventFormModel actualFormModel = new EventFormModel()
            {
                Title = Even.Title,
                Address = Even.Address,
                Description = Even.Description,
                ImageUrl = Even.ImageUrl,
                Tax = Even.Tax,
                EventCategoryId = Even.EventCategoryId,
                Start = Even.Start,
                End = Even.End
            };

            await this.eventService.EditEventByIdAndFormModelAsync(eventId, actualFormModel);

            Assert.IsTrue(this.dbContext.Events.Any(e => e.Id.ToString() == eventId));
        }

        // GetEventForDeleteByIdAsync Test
        [Test]
        public async Task GetEventForDeleteByIdAsyncShouldReturnCorrectEventPreDeleteDetailsViewModel()
        {
            string existEventId = Even.Id.ToString();
            EventPreDeleteDetailsViewModel actualEventPreDeleteDetailsViewModel = new EventPreDeleteDetailsViewModel()
            {
                Title = Even.Title,
                Address = Even.Address,
                ImageUrl = Even.ImageUrl
            };

            EventPreDeleteDetailsViewModel expectedEventPreDeleteDetailsViewModel = await this.eventService.GetEventForDeleteByIdAsync(existEventId);

            Assert.IsTrue(actualEventPreDeleteDetailsViewModel.Title == expectedEventPreDeleteDetailsViewModel.Title &&
                       actualEventPreDeleteDetailsViewModel.Address == expectedEventPreDeleteDetailsViewModel.Address &&
                       actualEventPreDeleteDetailsViewModel.ImageUrl == expectedEventPreDeleteDetailsViewModel.ImageUrl);
        }
        [Test]
        public void GetEventForDeleteByIdAsyncShouldReturnExceptionIfEventIdNotExist()
        {
            string existEventId = "7631b694-2f12-4d74-bab5-dc90a32f4adf";

            Assert.ThrowsAsync<InvalidOperationException>(async () => await this.eventService.GetEventForDeleteByIdAsync(existEventId));
        }

        // DeleteEventByIdAsync Test
        [Test]
        public async Task DeleteEventByIdAsyncShouldDeleteEventSuccessfully()
        {
            string existEventId = Even2.Id.ToString();

            await this.eventService.DeleteEventByIdAsync(existEventId);

            bool result = this.dbContext.Events.Any(e => e.Id.ToString() == existEventId && e.IsActive == false);

            Assert.IsTrue(result);
        }
        [Test]
        public void DeleteEventByIdAsyncShouldReturnExceptionIfEventNotExist()
        {
            string existEventId = "d5c653e3-1a69-478d-b3c1-5c0d78fa5105";

            Assert.ThrowsAsync<InvalidOperationException>(async () => await this.eventService.DeleteEventByIdAsync(existEventId));
        }

        // AddEventToMineAsync Test
        [Test]
        public void AddEventToMineAsyncShouldReturnExceptionIfUserIdNotExist()
        {
            string userId = "a924c9e9-8750-497d-a646-c949e36f20f4";

            CoachInfoOnEventViewModel coach = new CoachInfoOnEventViewModel()
            {
                FullName = $"{Coach.User.FirstName} {Coach.User.LastName}",
                Email = Coach.User.Email,
                PhoneNumber = Coach.PhoneNumber,
                Category = Coach.CoachCategoryId
            };

            EventDetailsViewModel even = new EventDetailsViewModel() 
            { 
                Description = Even.Description,
                Category = Even.EventCategory.Name,
                Start = Even.Start,
                End = Even.End,
                Coach = coach,
                Clients = 0
            };

            Assert.ThrowsAsync<ArgumentNullException>(async () => await this.eventService.AddEventToMineAsync(userId, even));
        }

        // GetEventDetailsByIdAsync Test
        [Test]
        public async Task GetEventDetailsByIdAsyncShouldReturnCorrectEventDetailsViewModel()
        {
            string existEventId = Even.Id.ToString();

            EventDetailsViewModel actualViewModel = new EventDetailsViewModel()
            {
                Id = Even.Id.ToString(),
                Title = Even.Title,
                Address = Even.Address,
                ImageUrl = Even.ImageUrl,
                Tax = Even.Tax,
                CoachName = $"{Even.Coach.User.FirstName} {Even.Coach.User.LastName}"
            };

            EventDetailsViewModel? expectedViewModel = await this.eventService.GetEventDetailsByIdAsync(existEventId);

            Assert.IsTrue(actualViewModel.Id == expectedViewModel?.Id &&
                        actualViewModel.Title == expectedViewModel?.Title &&
                        actualViewModel.Address == expectedViewModel?.Address &&
                        actualViewModel.ImageUrl == expectedViewModel?.ImageUrl &&
                        actualViewModel.Tax == expectedViewModel?.Tax &&
                        actualViewModel.CoachName == expectedViewModel?.CoachName);
        }

        // GetEventByIdAsync Test
        [Test]
        public async Task GetEventByIdAsyncShouldReturnCorrectEventIfEventIdExist()
        {
            string existEventId = Even.Id.ToString();

            var actualEvent = await dbContext
               .Events
               .Where(e => e.Id.ToString() == existEventId)
               .FirstAsync();

            var expectedEvent = await this.eventService.GetEventByIdAsync(existEventId);

            Assert.IsTrue(actualEvent.Id == expectedEvent.Id &&
                        actualEvent.IsActive == expectedEvent.IsActive &&
                        actualEvent.Address == expectedEvent.Address &&
                        actualEvent.Tax == expectedEvent.Tax &&
                        actualEvent.CoachId == expectedEvent.CoachId &&
                        actualEvent.End == expectedEvent.End &&
                        actualEvent.CreatedOn == expectedEvent.CreatedOn &&
                        actualEvent.Description == expectedEvent.Description &&
                        actualEvent.Start == expectedEvent.Start &&
                        actualEvent.EventCategoryId == expectedEvent.EventCategoryId &&
                        actualEvent.ImageUrl == expectedEvent.ImageUrl);
        }
        [Test]
        public void GetEventByIdAsyncShouldReturnExceptionIfEventIdNoExist()
        {
            string existEventId = "1675fa38-2462-4b36-8a6e-b7156203ff48";

            Assert.ThrowsAsync<InvalidOperationException>(async () => await this.eventService.GetEventByIdAsync(existEventId));
        }

        // IsEventExpired
        [Test]
        public async Task IsEventExpiredShouldReturnTrueIfEventNotExpired()
        {
            string existEventId = Even.Id.ToString();

            bool result = await this.eventService.IsEventExpired(existEventId);

            Assert.IsTrue(result);
        }
    }
}