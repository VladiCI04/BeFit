using BeFit.Data;
using BeFit.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace BeFit.Services.Tests
{
    public static class DatabaseSeeder
    {
        public static ApplicationUser CoachUser = null!;
        public static ApplicationUser ClientUser = null!;
        public static Coach Coach = null!;

        public static void SeedDtabase(BeFitDbContext dbContext)
        {
            CoachUser = new ApplicationUser()
            {
                UserName = "Test",
                NormalizedUserName = "Test",
                Email = "test@coaches.com",
                NormalizedEmail = "TEST@COACHES.COM",
                EmailConfirmed = true,
                PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
                ConcurrencyStamp = "e1d5baee-3aa0-4f26-a8b3-fdc3652dfa52",
                SecurityStamp = "a15747e3-aa78-4e4e-967d-7c0592155f21",
                TwoFactorEnabled = false,
                FirstName = "Test",
                LastName = "Testov",
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

            Coach = new Coach()
            {
                Age = 20,
                Gender = "Male",
                Height = 1.80,
                Weight = 80,
                PhoneNumber = "0888888888",
                Description = "Best test coach in the world!",
                User = CoachUser
            };

            dbContext.Users.Add(CoachUser);
            dbContext.Users.Add(ClientUser);
            dbContext.Coaches.Add(Coach);

            dbContext.SaveChanges();
        }
    }
}
