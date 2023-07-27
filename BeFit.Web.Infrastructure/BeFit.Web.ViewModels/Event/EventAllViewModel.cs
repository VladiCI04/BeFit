using BeFit.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace BeFit.Web.ViewModels.Event
{
	public class EventAllViewModel
	{
        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;

		public string Address { get; set; } = null!;

		[Display(Name = "Image Link")]
		public string ImageUrl { get; set; } = null!;

		public double Tax { get; set; }

		public string CoachName { get; set; } = null!;

		public ICollection<string> Clients { get; set; } = new List<string>();
    }
}
