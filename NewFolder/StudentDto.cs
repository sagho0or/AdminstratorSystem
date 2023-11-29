
using System.ComponentModel.DataAnnotations;

namespace AdministratorSystem.NewFolder
{
    public class StudentDto
    {
        public int StudentId { get; set; }
        public string FullName { get; set; }
        public StudentCourseDto Course { get; set; }

    }


    public class StudentCourseDto
    {
        public int CourseId { get; set; }
        public int DurationInYears { get; set; }
        public string Title { get; set; }
        public int? Mark { get; set; }
        public string Result { get; set; }
        public ICollection<StudentModuleDto>? Modules { get; set; }

    }

    public class StudentModuleDto
    {
        public int ModuleId { get; set; }
        public string Title { get; set; }
        public int? Mark { get; set; }
        public string? Result { get; set; }
        public bool IsRequired { get; set; }
        public ICollection<StudentAssessmentDto> Assessments { get; set; }

    }

    public class StudentAssessmentDto
    {
        public int AssessmentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Mark { get; set; }

    }
}
