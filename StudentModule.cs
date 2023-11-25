using System.Reflection;

namespace AdministratorSystem
{
    public class StudentModule
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int ModuleId { get; set; }
        public Module Module { get; set; }
        public int? Mark { get; set; }
        public string? Result { get; set; }
    }
}
