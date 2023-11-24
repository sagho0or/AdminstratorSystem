namespace AdministratorSystem
{
    ////Some of the modules of a programme are determined by that program and some are chosen as options by the student registered on that programme
    //No two degree programs may have the same set of modules.
    //The average mark across all the modules in a programme, as an integer percentage, is the programme mark

    public class Course
    {
        public int CourseId { get; set; }
        //begin with the year of the cohort - unique 6 digit
        public string CourseIdentifier { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int DurationInYears { get; set; }

        // Collection navigation property
        public ICollection<Cohort>? Cohorts { get; set; }
        public ICollection<CourseModule> CourseModules { get; set; } = new List<CourseModule>();

        public ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
