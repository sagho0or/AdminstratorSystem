namespace AdministratorSystem
{
    //A student is registered on a degree programme and becomes a member of a particular student cohort
     //A student cohort is the set of students registered for a given degree programme in a given academic
     //session (academic year)

    public class Student
    {
        // the ID begins with the year of the cohort followed by unique 6 digit string
        public string StundetIdentifier { get; set; }
        public int StudentId { get; set; }
        public string FullName { get; set; }
        public int CohortId { get; set; }
        public Cohort Cohort {  get; set; }
        public ICollection<StudentAssessment> StudentAssessments { get; set; }


    }
}
