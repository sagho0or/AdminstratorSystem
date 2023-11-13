namespace AdministratorSystem
{
    public class StudentAssesment
    {
        public int Id { get; set; }
        public Student Student { get; set; }
        public Assessment Assessment { get; set; }
        public int Score { get; set; }
    }
}
