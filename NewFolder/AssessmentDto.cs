using System.ComponentModel.DataAnnotations;

namespace AdministratorSystem.NewFolder
{
    public class AssessmentDto
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "MaximumMark is required")]
        [Range(0, 100, ErrorMessage = "The value must be between 0 and 100.")]
        public int MaximumMark { get; set; }

    }
}
