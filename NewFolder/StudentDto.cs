
using System.ComponentModel.DataAnnotations;

namespace AdministratorSystem.NewFolder
{
    public class StudentDto
    {
        [Required(ErrorMessage = "Student full name is required")]
        public string FullName { get; set; }

    }
}
