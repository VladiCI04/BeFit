using BeFit.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeFit.Data.Configurations
{
	public class SeedCoachesEntityConfiguration : IEntityTypeConfiguration<Coach>
	{
		public void Configure(EntityTypeBuilder<Coach> builder)
		{
			builder.HasData(this.GenerateCoaches());
		}

		private Coach[] GenerateCoaches()
		{
			ICollection<Coach> coaches = new HashSet<Coach>();

			Coach coach;

			coach = new Coach()
			{
				Id = Guid.Parse("66f0823e-09a4-4858-8f47-a8e096c859b9"),
				Age = 21,
				Gender = "Female",
				Height = 1.65,
				Weight = 61,
				PhoneNumber = "0886810542",
				Description = "Very serious and hardworking coach!",
				CoachCategoryId = 5,
				UserId = Guid.Parse("f4f678ce-62d4-4dde-97cf-e1de3f4e7482")
			};
			coaches.Add(coach);

			coach = new Coach()
			{
				Id = Guid.Parse("b2d36361-646f-496a-a5a7-26b9ed2a1a33"),
				Age = 27,
				Gender = "Male",
				Height = 1.90,
				Weight = 100,
				PhoneNumber = "0886810378",
				Description = "Very serious and hardworking coach!",
				CoachCategoryId = 2,
				UserId = Guid.Parse("0a7141a1-62c8-4a1f-9225-1d77f76412d1")
			};
			coaches.Add(coach);

			coach = new Coach()
			{
				Id = Guid.Parse("48d78e23-9007-4871-8d89-b8acb2c7e2e8"),
				Age = 30,
				Gender = "Other",
				Height = 1.80,
				Weight = 85,
				PhoneNumber = "0886810123",
				Description = "Admin admin admin",
				CoachCategoryId = 5,
				UserId = Guid.Parse("0bee301e-6e95-41ae-aa91-e8dc87112eea")
			};
			coaches.Add(coach);

			return coaches.ToArray();
		} 
	}
}
