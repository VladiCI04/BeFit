using BeFit.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeFit.Data.Configurations
{
	public class SeedEventsEntityConfiguration : IEntityTypeConfiguration<Event>
	{
		public void Configure(EntityTypeBuilder<Event> builder)
		{
			builder.HasData(this.GenerateEvents());
		}

		private Event[] GenerateEvents()
		{
			ICollection<Event> events = new HashSet<Event>();

			Event even;

			even = new Event()
			{
				Id = Guid.Parse("73482205-eb8f-49a8-b622-1127f292c3ec"),
				Title = "Rugby",
				Description = "Best rugby event ever!",
				Address = "Ivan Vazov 51",
				ImageUrl = "https://www.barnstaplerugby.co.uk/wp-content/uploads/2021/10/chiefs-v-westcliff101.jpg",
				Tax = 40,
				CoachId = Guid.Parse("48d78e23-9007-4871-8d89-b8acb2c7e2e8"),
				CreatedOn = DateTime.Parse("2023-08-06 01:06:22.3300000"),
				IsActive = true,
				Start = DateTime.Parse("2023-08-06 01:06:00.0000000"),
				End = DateTime.Parse("2023-08-17 01:06:00.0000000"),
				EventCategoryId = 1
			};
			events.Add(even);

			even = new Event()
			{
				Id = Guid.Parse("c51963d9-8074-423f-bcc6-15b60a35cecf"),
				Title = "Athletics",
				Description = "Best athletics event ever!",
				Address = "Hristo Botev 15",
				ImageUrl = "https://www.surreynowleader.com/wp-content/uploads/2019/08/18131021_web1_OceanAthletics-Olivia-van-Ryswyk-web.jpg",
				Tax = 15.50,
				CoachId = Guid.Parse("66f0823e-09a4-4858-8f47-a8e096c859b9"),
				CreatedOn = DateTime.Parse("2023-08-06 01:06:22.3300000"),
				IsActive = true,
				Start = DateTime.Parse("2023-08-06 01:06:00.0000000"),
				End = DateTime.Parse("2023-08-17 01:06:00.0000000"),
				EventCategoryId = 1
			};
			events.Add(even);

			even = new Event()
			{
				Id = Guid.Parse("e2c4742f-7b1b-4e43-b574-af3fca09697c"),
				Title = "Chess",
				Description = "Best chess event ever!",
				Address = "Hristo Smirnenski 63",
				ImageUrl = "https://www.bworldonline.com/wp-content/uploads/2021/09/white-chess-pieces-chess-board-king.jpg",
				Tax = 31.20,
				CoachId = Guid.Parse("b2d36361-646f-496a-a5a7-26b9ed2a1a33"),
				CreatedOn = DateTime.Parse("2023-08-06 01:06:22.3300000"),
				IsActive = true,
				Start = DateTime.Parse("2023-08-06 01:06:00.0000000"),
				End = DateTime.Parse("2023-08-17 01:06:00.0000000"),
				EventCategoryId = 2
			};
			events.Add(even);

			even = new Event()
			{
				Id = Guid.Parse("127cb9cf-490d-4baa-834a-f0bb5f9304f9"),
				Title = "Board game",
				Description = "Best board game event ever!",
				Address = "Kokiche 21",
				ImageUrl = "https://perfectescaperoom.com/wp-content/uploads/2020/09/Untitled-design-62.png",
				Tax = 28.30,
				CoachId = Guid.Parse("b2d36361-646f-496a-a5a7-26b9ed2a1a33"),
				CreatedOn = DateTime.Parse("2023-08-06 01:06:22.3300000"),
				IsActive = true,
				Start = DateTime.Parse("2023-08-06 01:06:00.0000000"),
				End = DateTime.Parse("2023-08-17 01:06:00.0000000"),
				EventCategoryId = 2
			};
			events.Add(even);

			even = new Event()
			{
				Id = Guid.Parse("08cd5700-99cb-4843-b5c4-f70df2ca4deb"),
				Title = "Car racing",
				Description = "Best car racing event ever!",
				Address = "Vasil Levski 118",
				ImageUrl = "https://di-uploads-pod23.dealerinspire.com/lexusoflasvegas/uploads/2021/08/Lexus-Racing-Lime-Rock.jpg",
				Tax = 50,
				CoachId = Guid.Parse("b2d36361-646f-496a-a5a7-26b9ed2a1a33"),
				CreatedOn = DateTime.Parse("2023-08-06 01:06:22.3300000"),
				IsActive = true,
				Start = DateTime.Parse("2023-08-06 01:06:00.0000000"),
				End = DateTime.Parse("2023-08-17 01:06:00.0000000"),
				EventCategoryId = 3
			};
			events.Add(even);

			even = new Event()
			{
				Id = Guid.Parse("3a2c6e63-e82f-4fc9-94da-9309d90b2578"),
				Title = "Powerboating",
				Description = "Best powerboating event ever!",
				Address = "Stoyan Zaimov 35",
				ImageUrl = "https://i0.wp.com/397566-www.web.tornado-node.net/wp-content/uploads/2021/11/RAF1074-scaled.jpg?resize=1080%2C720&ssl=1",
				Tax = 45.60,
				CoachId = Guid.Parse("66f0823e-09a4-4858-8f47-a8e096c859b9"),
				CreatedOn = DateTime.Parse("2023-08-06 01:06:22.3300000"),
				IsActive = true,
				Start = DateTime.Parse("2023-08-06 01:06:00.0000000"),
				End = DateTime.Parse("2023-08-17 01:06:00.0000000"),
				EventCategoryId = 3
			};
			events.Add(even);

			even = new Event()
			{
				Id = Guid.Parse("110e4a17-9885-457b-b755-6bfcf69ccc17"),
				Title = "Billiards",
				Description = "Best billiards event ever!",
				Address = "Geo Milev 100",
				ImageUrl = "https://i.ebayimg.com/images/g/B6QAAOSwTLBjBSwy/s-l1200.jpg",
				Tax = 15.20,
				CoachId = Guid.Parse("66f0823e-09a4-4858-8f47-a8e096c859b9"),
				CreatedOn = DateTime.Parse("2023-08-06 01:06:22.3300000"),
				IsActive = true,
				Start = DateTime.Parse("2023-08-06 01:06:00.0000000"),
				End = DateTime.Parse("2023-08-17 01:06:00.0000000"),
				EventCategoryId = 4
			};
			events.Add(even);

			even = new Event()
			{
				Id = Guid.Parse("d2aaedfe-e4b4-4897-ba75-4cef430b8b93"),
				Title = "Tennis",
				Description = "Best tennis event ever!",
				Address = "Petko Karavelov 16",
				ImageUrl = "https://tennisalberta.com/wp-content/uploads/2021/11/Presentation-Lifestyle_Penn.jpg",
				Tax = 29.50,
				CoachId = Guid.Parse("66f0823e-09a4-4858-8f47-a8e096c859b9"),
				CreatedOn = DateTime.Parse("2023-08-06 01:06:22.3300000"),
				IsActive = true,
				Start = DateTime.Parse("2023-08-06 01:06:00.0000000"),
				End = DateTime.Parse("2023-08-17 01:06:00.0000000"),
				EventCategoryId = 4
			};
			events.Add(even);

			even = new Event()
			{
				Id = Guid.Parse("686e191d-946a-4542-ae8f-b9c82c59b054"),
				Title = "Equestrian",
				Description = "Best equestrian event ever!",
				Address = "Ivan Milev 1",
				ImageUrl = "https://www.cavalletti.com.au/wp-content/uploads/2022/11/FB_IMG_1667257848544.jpg",
				Tax = 22.30,
				CoachId = Guid.Parse("b2d36361-646f-496a-a5a7-26b9ed2a1a33"),
				CreatedOn = DateTime.Parse("2023-08-06 01:06:22.3300000"),
				IsActive = true,
				Start = DateTime.Parse("2023-08-06 01:06:00.0000000"),
				End = DateTime.Parse("2023-08-17 01:06:00.0000000"),
				EventCategoryId = 5
			};
			events.Add(even);

			even = new Event()
			{
				Id = Guid.Parse("a166347c-7050-468a-b046-0bbc681d74ae"),
				Title = "Rodeo",
				Description = "Best rodeo event ever!",
				Address = "Petko Ivanov 18",
				ImageUrl = "https://www.rubysinn.com/wp-content/uploads/2014/11/things-to-do-in-bryce-canyon-rodeo.jpg",
				Tax = 35.75,
				CoachId = Guid.Parse("b2d36361-646f-496a-a5a7-26b9ed2a1a33"),
				CreatedOn = DateTime.Parse("2023-08-06 01:06:22.3300000"),
				IsActive = true,
				Start = DateTime.Parse("2023-08-06 01:06:00.0000000"),
				End = DateTime.Parse("2023-08-17 01:06:00.0000000"),
				EventCategoryId = 5
			};
			events.Add(even);

			return events.ToArray();
		}
	}
}
