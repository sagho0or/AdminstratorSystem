using System.Reflection;

namespace AdministratorSystem
{
    //Each module contains one or more assessments
    //An assessment may be shared across more than one module

    // A module result is undefined if there is no mark for any assessment in that module
    public class Module
    {
        public int ModuleId { get; set; }
        public int ModuleCode { get; set; }
        public string Title { get; set; } = string.Empty;

        //TODO Compulsory/optional
        public bool IsRequired { get; set; }

        // Navigation property one to many
        public Course? Course { get; set; }

        public ICollection<CourseModule> CourseModules { get; set; }

        public ICollection<ModuleAssessment> ModuleAssessments { get; set; } = new List<ModuleAssessment>(2);

        public ICollection<StudentModule> StudentModules { get; set; }
        // TODO list of one or two
        //public Assignemnt Assignent { get; set; }


    }
}
