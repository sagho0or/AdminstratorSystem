using System.ComponentModel.DataAnnotations;

namespace AdministratorSystem.NewFolder
{
    public class CohortDto
    {
        [Required(ErrorMessage = "Cohort AcademicYear is required")]
        [RegularExpression(@"^(19|20)\d{2}$", ErrorMessage = "Academic year should be a valid year")]
        public string AcademicYear { get; set; }

    }
}
