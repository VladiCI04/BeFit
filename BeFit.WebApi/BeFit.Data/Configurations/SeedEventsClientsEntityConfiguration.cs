using BeFit.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeFit.Data.Configurations
{
	public class SeedEventsClientsEntityConfiguration : IEntityTypeConfiguration<EventClient>
	{
		public void Configure(EntityTypeBuilder<EventClient> builder)
		{
			builder.HasData(this.GenerateEventsClients());
		}

		private EventClient[] GenerateEventsClients()
		{
			ICollection<EventClient> eventsClients = new HashSet<EventClient>();

			EventClient eventClient;

			eventClient = new EventClient() 
			{
				ClientId = Guid.Parse("40ab26f0-ce65-4276-8bf9-4ce80bbf256a"),
				EventId = Guid.Parse("e2c4742f-7b1b-4e43-b574-af3fca09697c")
			};
			eventsClients.Add(eventClient);

			eventClient = new EventClient()
			{
				ClientId = Guid.Parse("283c422e-8e6e-450b-818e-65d8d4c9426c"),
				EventId = Guid.Parse("e2c4742f-7b1b-4e43-b574-af3fca09697c")
			};
			eventsClients.Add(eventClient);

			eventClient = new EventClient()
			{
				ClientId = Guid.Parse("7ca25b19-34e1-4b20-b3e9-aa98e43bf574"),
				EventId = Guid.Parse("a166347c-7050-468a-b046-0bbc681d74ae")
			};
			eventsClients.Add(eventClient);

			eventClient = new EventClient()
			{
				ClientId = Guid.Parse("7ca25b19-34e1-4b20-b3e9-aa98e43bf574"),
				EventId = Guid.Parse("110e4a17-9885-457b-b755-6bfcf69ccc17")
			};
			eventsClients.Add(eventClient);

			eventClient = new EventClient()
			{
				ClientId = Guid.Parse("0bee301e-6e95-41ae-aa91-e8dc87112eea"),
				EventId = Guid.Parse("e2c4742f-7b1b-4e43-b574-af3fca09697c")
			};
			eventsClients.Add(eventClient);

			return eventsClients.ToArray();
		}
	}
}
