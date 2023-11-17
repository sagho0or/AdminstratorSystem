namespace AdministratorSystem
{
    public class CourseModule
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public int ModuleId { get; set; }
        public Module Module { get; set; }
        public bool IsRequired { get; set; }
    }

}
