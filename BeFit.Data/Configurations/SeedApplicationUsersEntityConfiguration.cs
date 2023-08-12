using BeFit.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BeFit.Data.Configurations
{
	public class SeedApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
	{
		public void Configure(EntityTypeBuilder<ApplicationUser> builder)
		{
			builder.HasData(this.GenerateUsers());
		}

		private ApplicationUser[] GenerateUsers()
		{
			ICollection<ApplicationUser> users = new HashSet<ApplicationUser>();

			ApplicationUser user;

			user = new ApplicationUser()
			{
				Id = Guid.Parse("40ab26f0-ce65-4276-8bf9-4ce80bbf256a"),
				UserName = "quan@user.com",
				NormalizedUserName = "QUAN@USER.COM",
				Email = "quan@user.com",
				NormalizedEmail = "QUAN@USER.COM",
				EmailConfirmed = true,
				PasswordHash = "AQAAAAEAACcQAAAAEK/RAYa6evbIhLVitkLGMG1dwASATbFzp2kLlzikIG0ecKbN9ToCnrmcHoltGDWq6w==",
				SecurityStamp = "f4836823-07e9-4cd3-aad7-654992ef7c28",
				ConcurrencyStamp = "318a8581-71c6-4bc4-8d8f-61459e946cc5",
				PhoneNumber = null,
				PhoneNumberConfirmed = false,
				TwoFactorEnabled = false,
				FirstName = "Quan",
				LastName = "Rodriguez"
			};
			users.Add(user);

			user = new ApplicationUser()
			{
				Id = Guid.Parse("283c422e-8e6e-450b-818e-65d8d4c9426c"),
				UserName = "bentley@user.com",
				NormalizedUserName = "BENTLEY@USER.COM",
				Email = "bentley@user.com",
				NormalizedEmail = "BENTLEY@USER.COM",
				EmailConfirmed = true,
				PasswordHash = "AQAAAAEAACcQAAAAEK/RAYa6evbIhLVitkLGMG1dwASATbFzp2kLlzikIG0ecKbN9ToCnrmcHoltGDWq6w==",
				SecurityStamp = "86002846-7ecb-4034-9dce-963e10002c88",
				ConcurrencyStamp = "5d9bb0dd-fd77-4410-b59e-88752db70867",
				PhoneNumber = null,
				PhoneNumberConfirmed = false,
				TwoFactorEnabled = false,
				FirstName = "Bentley",
				LastName = "Ivanov"
			};
			users.Add(user);

			user = new ApplicationUser()
			{
				Id = Guid.Parse("7ca25b19-34e1-4b20-b3e9-aa98e43bf574"),
				UserName = "lenlen@user.com",
				NormalizedUserName = "LENLEN@USER.COM",
				Email = "lenlen@user.com",
				NormalizedEmail = "LENLEN@USER.COM",
				EmailConfirmed = true,
				PasswordHash = "AQAAAAEAACcQAAAAEK/RAYa6evbIhLVitkLGMG1dwASATbFzp2kLlzikIG0ecKbN9ToCnrmcHoltGDWq6w==",
				SecurityStamp = "41018a16-6966-449c-8616-3850c774c020",
				ConcurrencyStamp = "cc6ca61d-0372-491f-b8f2-b50404aaf552",
				PhoneNumber = null,
				PhoneNumberConfirmed = false,
				TwoFactorEnabled = false,
				FirstName = "Leonardo",
				LastName = "Dicaprio"
			};
			users.Add(user);

			user = new ApplicationUser()
			{
				Id = Guid.Parse("0a7141a1-62c8-4a1f-9225-1d77f76412d1"),
				UserName = "sami@coach.com",
				NormalizedUserName = "SAMI@COACH.COM",
				Email = "sami@coach.com",
				NormalizedEmail = "SAMI@COACH.COM",
				EmailConfirmed = true,
				PasswordHash = "AQAAAAEAACcQAAAAEK/RAYa6evbIhLVitkLGMG1dwASATbFzp2kLlzikIG0ecKbN9ToCnrmcHoltGDWq6w==",
				SecurityStamp = "f5738f4e-c5bd-44b2-9234-87aba4891a52",
				ConcurrencyStamp = "3126e7a6-a4cc-4433-b6ce-1e52f6aa47ea",
				PhoneNumber = null,
				PhoneNumberConfirmed = false,
				TwoFactorEnabled = false,
				FirstName = "Sami",
				LastName = "Hosni"
			};
			users.Add(user);

			user = new ApplicationUser()
			{
				Id = Guid.Parse("f4f678ce-62d4-4dde-97cf-e1de3f4e7482"),
				UserName = "lenyg@coach.com",
				NormalizedUserName = "LENYG@COACH.COM",
				Email = "lenyg@coach.com",
				NormalizedEmail = "LENYG@COACH.COM",
				EmailConfirmed = true,
				PasswordHash = "AQAAAAEAACcQAAAAEK/RAYa6evbIhLVitkLGMG1dwASATbFzp2kLlzikIG0ecKbN9ToCnrmcHoltGDWq6w==",
				SecurityStamp = "6823fac7-e949-454c-9186-526180a38e4a",
				ConcurrencyStamp = "47aa19c2-5c5c-4030-80f5-23bc42c92dce",
				PhoneNumber = null,
				PhoneNumberConfirmed = false,
				TwoFactorEnabled = false,
				FirstName = "Elena",
				LastName = "Georgieva"
			};
			users.Add(user);

			user = new ApplicationUser()
			{
				Id = Guid.Parse("0bee301e-6e95-41ae-aa91-e8dc87112eea"),
				UserName = "admin@befit.bg",
				NormalizedUserName = "ADMIN@BEFIT.BG",
				Email = "admin@befit.bg",
				NormalizedEmail = "ADMIN@BEFIT.BG",
				EmailConfirmed = true,
				PasswordHash = "AQAAAAEAACcQAAAAEK/RAYa6evbIhLVitkLGMG1dwASATbFzp2kLlzikIG0ecKbN9ToCnrmcHoltGDWq6w==",
				SecurityStamp = "3c8597c2-c491-4c71-8895-ca8ff5f6d25d",
				ConcurrencyStamp = "504b4b8e-770e-4cfc-a695-cac16a690499",
				PhoneNumber = null,
				PhoneNumberConfirmed = false,
				TwoFactorEnabled = false,
				FirstName = "Admin",
				LastName = "Admin"
			};
			users.Add(user);

			var password = new PasswordHasher<ApplicationUser>();
			var hashed = password.HashPassword(user, "123456");
			user.PasswordHash = hashed;

			return users.ToArray();	
		}
	}
}
