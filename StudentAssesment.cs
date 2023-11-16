namespace AdministratorSystem
{
    public class StudentAssesment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int AssessmentId { get; set; }
        public Assessment Assessment { get; set; }
        public int Mark { get; set; }
    }
}
