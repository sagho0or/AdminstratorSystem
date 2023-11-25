using System.Reflection;

namespace AdministratorSystem
{
    public class Module
    {
        public int ModuleId { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsRequired { get; set; }
        public Course? Course { get; set; }
        public ICollection<CourseModule> CourseModules { get; set; }
        public ICollection<ModuleAssessment> ModuleAssessments { get; set; } = new List<ModuleAssessment>();
        public ICollection<StudentModule> StudentModules { get; set; }

    }
}
