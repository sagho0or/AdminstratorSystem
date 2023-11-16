namespace AdministratorSystem.NewFolder
{
    public class CourseDto
    {
        [Required(ErrorMessage = "Course Title is required")]
        public string Title { get; set; }

        [Range(1, 3, ErrorMessage = "Duration in Years should be between 1 and 3")]
        public int DurationInYears { get; set; }
    }
}
