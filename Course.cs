namespace AdministratorSystem
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int DurationInYears { get; set; }

        // Collection navigation property
        public ICollection<Cohort>? Cohorts { get; set; }
        public ICollection<CourseModule> CourseModules { get; set; } = new List<CourseModule>();

        public ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
