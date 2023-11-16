using System.Reflection;

namespace AdministratorSystem
{
    //Each module contains one or more assessments
    //An assessment may be shared across more than one module

    // A module result is undefined if there is no mark for any assessment in that module
    public class Module
    {
        public int ModuleId { get; set; }

        //TODO 5digit
        public int ModuleCode { get; set; }
        public string Title { get; set; } = string.Empty;

        //TODO Compulsory/optional
        public bool IsRequired { get; set; }
        public int CourseId { get; set; }


        // Navigation property one to many
        public Course? Course { get; set; }
        public ICollection<Assessment>? Assessments { get; set; } = new List<Assessment>(2);

        // TODO list of one or two
        //public Assignemnt Assignent { get; set; }

        //Question value of Mark? 
        public int? Mark => CalculateModuleMark();

        public int? CalculateModuleMark()
        {
            if (Assessments == null || Assessments.Count == 0)
            {
                // No assessments, mark is undefined
                return null;
            }

            int totalWeightedMarks = 0;
            int assessmentCount = 0;

            foreach (var assessment in Assessments)
            {
                if (assessment.Mark.HasValue)
                {
                    totalWeightedMarks += assessment.Mark.Value;
                    assessmentCount++;
                }
                else
                {
                    // assessment mark is undefined, module mark is undefined
                    return null;
                }
            }

            return totalWeightedMarks / assessmentCount;
        }

        public string Result => GetModuleResult();
        // The module result is Pass if the module mark is greater than or equal to 50.
        // If the module mark is less than 50 but greater than or equal to 45 then the module result is PassCompensation.
        // Otherwise the module result is Fail
        public string GetModuleResult()
        {
            var moduleMark = CalculateModuleMark();

            if (!moduleMark.HasValue)
            {
                // Program mark is undefined
                return "Undefined";
            }

            if (moduleMark >= 50)
            {
                return "Pass";
            }
            else if (moduleMark >= 45)
            {
                return "PassCompensation";
            }
            else
            {
                return "Fail";
            }
        }

    }
}
