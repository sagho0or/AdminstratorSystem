using System.ComponentModel.DataAnnotations;

namespace AdministratorSystem.NewFolder
{
    public class ModuleAssessmentDto
    {
        [Required(ErrorMessage = "MaximumMark is required")]
        [Range(0, 100, ErrorMessage = "The value must be between 0 and 100.")]
        public int MaxMark { get; set; }
        [Required(ErrorMessage = "AssessmentId is required")]
        public int AssessmentId { get; set; }
        [Required(ErrorMessage = "ModuleId is required")]
        public int ModuleId { get; set; }
        

    }
}
