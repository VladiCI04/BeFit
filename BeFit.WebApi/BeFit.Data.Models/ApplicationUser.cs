using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static BeFit.Common.EntityValidationConstants.User;

namespace BeFit.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
        }

        [Required]
        [MinLength(UserFirstNameMinLength)]
        [MaxLength(UserFirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
		[MinLength(UserLastNameMinLength)]
		[MaxLength(UserLastNameMaxLength)]
		public string LastName { get; set; } = null!;

        public virtual ICollection<Event> Events { get; set; } = new List<Event>();   
    }
}
