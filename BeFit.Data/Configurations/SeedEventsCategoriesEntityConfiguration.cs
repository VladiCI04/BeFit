using BeFit.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeFit.Data.Configurations
{
	public class SeedEventsCategoriesEntityConfiguration : IEntityTypeConfiguration<EventCategory>
	{
		public void Configure(EntityTypeBuilder<EventCategory> builder)
		{
			builder.HasData(this.GenerateEventsCategories());
		}

		private EventCategory[] GenerateEventsCategories()
		{
			ICollection<EventCategory> eventsCategories = new HashSet<EventCategory>();

			EventCategory eventCategory;

			eventCategory = new EventCategory()
			{
				Id = 1,
				Name = "Physical"
			};
			eventsCategories.Add(eventCategory);

			eventCategory = new EventCategory()
			{
				Id = 2,
				Name = "Mind"
			};
			eventsCategories.Add(eventCategory);

			eventCategory = new EventCategory()
			{
				Id = 3,
				Name = "Motorized"
			};
			eventsCategories.Add(eventCategory);

			eventCategory = new EventCategory()
			{
				Id = 4,
				Name = "Coordination"
			};
			eventsCategories.Add(eventCategory);

			eventCategory = new EventCategory()
			{
				Id = 5,
				Name = "Animal-supported"
			};
			eventsCategories.Add(eventCategory);

			return eventsCategories.ToArray();
		}
	}
}
