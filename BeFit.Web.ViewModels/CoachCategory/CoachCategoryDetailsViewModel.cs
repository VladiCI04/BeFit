using BeFit.Web.ViewModels.CoachCategory.Interfaces;

namespace BeFit.Web.ViewModels.CoachCategory
{
	public class CoachCategoryDetailsViewModel : ICoachCategoryDetailsModel
	{
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int Coaches { get; set; } = 0;
    }
}
