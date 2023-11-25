namespace AdministratorSystem
{
     public class Assessment
    {
        public int AssessmentId { get; set; }
        public string Title { get; set; } 
        public string Description { get; set; } 
        public ICollection<StudentAssessment> StudentAssessments { get; set; }
        public ICollection<ModuleAssessment> ModuleAssessments { get; set; }
        public ICollection<Module> Modules { get; set; }
        public Module? Module { get; set; }

    }
}
