using BeFit.Data;
using BeFit.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace BeFit.Services.Tests
{
    public static class DatabaseSeeder
    {
        public static ApplicationUser ClientUser = null!;
        public static ApplicationUser ClientUser2 = null!;

        public static ApplicationUser CoachUser = null!;
        public static ApplicationUser CoachUser2 = null!;

        public static EventCategory EventCategory = null!;

        public static CoachCategory CoachCategory = null!;

        public static Event Even = null!;
        public static Event Even2 = null!;

        public static Coach Coach = null!;
        public static Coach Coach2 = null!;

        public static EventClient EventClient = null!;

        public static void SeedDtabase(BeFitDbContext dbContext)
        {
            CoachUser = new ApplicationUser()
            {
                UserName = "Test",
                NormalizedUserName = "Test",
                Email = "test@coaches.com",
                NormalizedEmail = "TEST@COACHES.COM",
                EmailConfirmed = true,
                PhoneNumber = "0111111111",
                PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
                ConcurrencyStamp = "e1d5baee-3aa0-4f26-a8b3-fdc3652dfa52",
                SecurityStamp = "a15747e3-aa78-4e4e-967d-7c0592155f21",
                TwoFactorEnabled = false,
                FirstName = "Test",
                LastName = "Testov",
            };
            CoachUser2 = new ApplicationUser()
            {
                UserName = "Test2",
                NormalizedUserName = "Test2",
                Email = "testtwo@coaches.com",
                NormalizedEmail = "TESTTWO@COACHES.COM",
                EmailConfirmed = true,
                PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
                ConcurrencyStamp = "ba36d0ca-7b58-48e3-a264-40ceebf11436",
                SecurityStamp = "a8563407-8639-4cc4-b851-891d1ef21701",
                TwoFactorEnabled = false,
                FirstName = "Test2",
                LastName = "Testov2",
            };

            ClientUser = new ApplicationUser()
            {
                UserName = "Test1",
                NormalizedUserName = "Test1",
                Email = "testone@coaches.com",
                NormalizedEmail = "TESTONE@COACHES.COM",
                EmailConfirmed = true,
                PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
                ConcurrencyStamp = "15ef11a5-23d2-42ea-8aa5-b3cc55cb295b",
                SecurityStamp = "fdc156ea-a791-4250-9a79-69f426bb6133",
                TwoFactorEnabled = false,
                FirstName = "Test1",
                LastName = "Testov1",
            };
            ClientUser2 = new ApplicationUser()
            {
                UserName = "Test3",
                NormalizedUserName = "Test3",
                Email = "testfour@coaches.com",
                NormalizedEmail = "TESTFOUR@COACHES.COM",
                EmailConfirmed = true,
                PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
                ConcurrencyStamp = "b73a434b-d59c-4b74-b0ee-f2baf6fc1bac",
                SecurityStamp = "93e42035-6305-4a27-83ad-51b0ee2708e2",
                TwoFactorEnabled = false,
                FirstName = "Test4",
                LastName = "Testov4",
            };

            EventCategory = new EventCategory()
            {
                Id = 1,
                Name = "Testing",
            };

            CoachCategory = new CoachCategory()
            {
                Id = 1,
                Name = "Testing",
            };

            Coach = new Coach()
            {
                Age = 20,
                Gender = "Male",
                Height = 1.80,
                Weight = 80,
                PhoneNumber = "0888888888",
                Description = "Best test coach in the world!",
                CoachCategoryId = CoachCategory.Id,
                User = CoachUser
            };
            Coach2 = new Coach()
            {
                Age = 18,
                Gender = "Female",
                Height = 1.80,
                Weight = 80,
                PhoneNumber = "0444444444",
                Description = "Best test coach in the world!",
                CoachCategoryId = CoachCategory.Id,
                User = CoachUser2
            };

            Even = new Event()
            {
                Title = "Test",
                Description = "Test Test Test Test",
                Address = "Test1",
                ImageUrl = "https://yogagames.org/wp-content/uploads/2023/06/YGS24-start.jpg",
                Tax = 10,
                CoachId = Coach.Id,
                CreatedOn = DateTime.Parse("2023-08-06 01:06:22.3300000"),
                IsActive = true,
                Start = DateTime.Parse("2023-08-06 01:06:00.0000000"),
                End = DateTime.Parse("2023-08-17 01:06:00.0000000"),
                EventCategoryId = EventCategory.Id
            };
            Even2 = new Event()
            {
                Title = "Test2",
                Description = "Test2 Test2 Test2 Test2",
                Address = "Test2",
                ImageUrl = "https://yogagames.org/wp-content/uploads/2023/06/YGS24-start.jpg",
                Tax = 12,
                CoachId = Coach2.Id,
                CreatedOn = DateTime.Parse("2023-08-06 01:06:22.3300000"),
                IsActive = true,
                Start = DateTime.Parse("2023-08-06 01:06:00.0000000"),
                End = DateTime.Parse("2023-08-17 01:06:00.0000000"),
                EventCategoryId = EventCategory.Id
            };

            EventClient = new EventClient()
            {
                ClientId = ClientUser.Id,
                EventId = Even.Id
            };

            dbContext.Users.Add(ClientUser);
            dbContext.Users.Add(ClientUser2);

            dbContext.Users.Add(CoachUser);
            dbContext.Users.Add(CoachUser2);       

            dbContext.EventCategories.Add(EventCategory);

            dbContext.CoachCategories.Add(CoachCategory);

            dbContext.Events.Add(Even);
            dbContext.Events.Add(Even2);

            dbContext.Coaches.Add(Coach);
            dbContext.Coaches.Add(Coach2);

            dbContext.EventClients.Add(EventClient);

            dbContext.SaveChanges();
        }
    }
}
