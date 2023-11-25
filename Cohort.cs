namespace AdministratorSystem
{
    public class Cohort
    {
        public int CohortId { get; set; }
        public string AcademicYear { get; set; }
        public ICollection<Student>? Students { get; set; }
        // Foreign key
        public int CourseId { get; set; }
        // Navigation property
        public Course? Course { get; set; }

    }
}
