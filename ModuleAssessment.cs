namespace AdministratorSystem
{
    public class ModuleAssessment
    {
        public int AssessmentId { get; set; }
        public Assessment Assessment { get; set; }
        public int ModuleId { get; set; }
        public Module Module { get; set; }
        public int MaxMark { get; set; }
    }

}
