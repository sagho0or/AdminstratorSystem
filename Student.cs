namespace AdministratorSystem
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FullName { get; set; }
        public int CohortId { get; set; }
        public Cohort Cohort {  get; set; }
        public ICollection<StudentAssessment> StudentAssessments { get; set; }
        public ICollection<StudentModule> StudentModules { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; }

    }
}
