using BeFit.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static BeFit.Common.GeneralApplicationConstants;

namespace BeFit.Data.Configurations
{
	public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
	{
		public void Configure(EntityTypeBuilder<ApplicationUser> builder)
		{
			builder
				.Property(u => u.FirstName)
				.HasDefaultValue("Test");

			builder
				.Property(u => u.LastName)
				.HasDefaultValue("Testov");
		}
    }
}
