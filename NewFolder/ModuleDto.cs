namespace AdministratorSystem.NewFolder
{
    public class ModuleDto
    {
        [Required(ErrorMessage = "Module code is required")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "The field must be a 5-digit number")]
        public int Code { get; set; }
        [Required(ErrorMessage = "Title Title is required")]
        public string Title { get; set; }
    }
 }
