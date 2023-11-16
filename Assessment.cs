namespace AdministratorSystem
{
     public class Assessment
    {
        //begin with the year of the cohort - unique 6 digit
        public string AssessmentId { get; set; }
        public string Title { get; set; } 
        public string Description { get; set; } 
        public int MaximumMark { get; set; }
        public ICollection<StudentAssessment> StudentAssessments { get; set; }
        
        // Foreign key
        public int ModuleId { get; set; }
        public Module? Module { get; set; }

    }
}
