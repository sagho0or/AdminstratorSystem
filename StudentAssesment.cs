namespace AdministratorSystem
{
    public class StudentAssessment
    {
        public int id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int AssessmentId { get; set; }
        public Assessment Assessment { get; set; }
        public int ModuleId { get; set; }
        public Module Module { get; set; }
        public int? Mark { get; set; }
    }
}
