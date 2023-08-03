using System.ComponentModel.DataAnnotations;
using static BeFit.Common.EntityValidationConstants.CoachCategory;

namespace BeFit.Data.Models
{
    public class CoachCategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(CoachCategoryNameMinLength)]
        [MaxLength(CoachCategoryNameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Coach> Coaches { get; set; } = new List<Coach>();
    }
}
