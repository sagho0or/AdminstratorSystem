using System.ComponentModel.DataAnnotations;

namespace AdministratorSystem.NewFolder
{
    public class AssessmentDto
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "description is required")]
        public string Description { get; set; }

    }
}
