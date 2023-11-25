using System.ComponentModel.DataAnnotations;

namespace AdministratorSystem.NewFolder
{
    public class ModuleDto
    {
        [Required(ErrorMessage = "Title Title is required")]
        public string Title { get; set; }
    }
}
