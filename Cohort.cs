namespace AdministratorSystem
{
    ////Some of the modules of a programme are determined by that program and some are chosen as options by the student registered on that programme
    //No two degree programs may have the same set of modules.
    //The average mark across all the modules in a programme, as an integer percentage, is the programme mark
    //The programme mark is undefined if any module mark is undefined.
    //The programme result is Pass if the programme mark is greater than or equal to 50,
    //Distinction if the programme mark is greater than or equal to 70 and Fail if less than 50.
    public class Cohort
    {
        public Cohort(int academicYear)
        {
            AcademicYear = academicYear;
        }
        public int CohortId { get; set; }
        public int AcademicYear { get; set; }
        public ICollection<Student>? Students { get; set; }
        // Foreign key
        public int CourseId { get; set; }
        // Navigation property
        public Course? Course { get; set; }

    }
}
