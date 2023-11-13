namespace AdministratorSystem
{
    ////Some of the modules of a programme are determined by that program and some are chosen as options by the student registered on that programme
    //No two degree programs may have the same set of modules.
    //The average mark across all the modules in a programme, as an integer percentage, is the programme mark
    
    
    public class Course
    {
        public Course(string name, int durationInYears, ICollection<Module> modules)
        {
            Name = name;
            DurationInYears = durationInYears;
            Modules = modules;
        }
        //begin with the year of the cohort - unique 6 digit
        public string CourseId { get; set; } = string.Empty;
        public string Name { get; set; }
        public int DurationInYears { get; set; }

        // Collection navigation property
        public ICollection<Module>? Modules { get; set; }

        // Collection navigation property
        public ICollection<Cohort>? Cohorts { get; set; }
        public ICollection<Student>? Students { get; set; }

        public int? Mark => CalculateCourseMark();
        public string Result => GetCourseResult();
        public int? CalculateCourseMark()
        {
            if (Modules == null || Modules.Count == 0)
            {
                // No modules, mark is undefined
                return null;
            }

            int totalWeightedMarks = 0;
            int moduleCount = 0;

            foreach (var module in Modules)
            {
                if (module.Mark.HasValue)
                {
                    totalWeightedMarks += module.Mark.Value;
                    moduleCount++; 
                }
                else
                {
                    // Module mark is undefined, course mark is undefined
                    return null;
                }
            }

            return totalWeightedMarks / moduleCount;
        }

        //The programme mark is undefined if any module mark is undefined.
        //The programme result is Pass if the programme mark is greater than or equal to 50,
        //Distinction if the programme mark is greater than or equal to 70 and Fail if less than 50.
        public string GetCourseResult()
        {
            var courseMark = CalculateCourseMark();

            if (!courseMark.HasValue)
            {
                // Program mark is undefined
                return "Undefined";
            }

            if (courseMark >= 70)
            {
                return "Distinction";
            }
            else if (courseMark >= 50)
            {
                return "Pass";
            }
            else
            {
                return "Fail";
            }
        }
    }
}
