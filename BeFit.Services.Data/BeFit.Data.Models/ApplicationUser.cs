using Microsoft.AspNetCore.Identity;

namespace BeFit.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
        }

        public virtual ICollection<Event> Events { get; set; } = new List<Event>();   
    }
}
