using BeFit.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeFit.Data.Configurations
{
	public class SeedCoachesCategoriesEntityConfiguration : IEntityTypeConfiguration<CoachCategory>
	{
		public void Configure(EntityTypeBuilder<CoachCategory> builder)
		{
			builder.HasData(this.GenerateCoachesCategories());
		}

		private CoachCategory[] GenerateCoachesCategories()
		{
			ICollection<CoachCategory> coachCategories = new HashSet<CoachCategory>();

			CoachCategory coachCategory;

			coachCategory = new CoachCategory()
			{
				Id = 1,
				Name = "Fitness trainer"
			};
			coachCategories.Add(coachCategory);

			coachCategory = new CoachCategory()
			{
				Id = 2,
				Name = "Lifestyle coach"
			};
			coachCategories.Add(coachCategory);

			coachCategory = new CoachCategory()
			{
				Id = 3,
				Name = "Sports coach"
			};
			coachCategories.Add(coachCategory);

			coachCategory = new CoachCategory()
			{
				Id = 4,
				Name = "Personal trainer"
			};
			coachCategories.Add(coachCategory);

			coachCategory = new CoachCategory()
			{
				Id = 5,
				Name = "Athletic trainer"
			};
			coachCategories.Add(coachCategory);

			coachCategory = new CoachCategory()
			{
				Id = 6,
				Name = "Wellness specialist"
			};
			coachCategories.Add(coachCategory);

			coachCategory = new CoachCategory()
			{
				Id = 7,
				Name = "Health coach"
			};
			coachCategories.Add(coachCategory);

			coachCategory = new CoachCategory()
			{
				Id = 8,
				Name = "Exercise specialist"
			};
			coachCategories.Add(coachCategory);

			coachCategory = new CoachCategory()
			{
				Id = 9,
				Name = "Bodybuilding coach"
			};
			coachCategories.Add(coachCategory);

			return coachCategories.ToArray();
		}
	}
}
