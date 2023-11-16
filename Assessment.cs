namespace AdministratorSystem
{
     public class Assessment
    {
        //begin with the year of the cohort - unique 6 digit
        public string AssessmentId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public int MaxMark { get; set; }

        private int? mark;

        // Property to represent the obtained mark for the module
        public int? Mark
        {
            get => mark;
            set
            {
                // Check if the provided value is within the range [0, 100]
                if (value.HasValue && (value < 0 || value > 100))
                {
                    throw new ArgumentOutOfRangeException("Mark must be between 0 and 100.");
                }

                mark = value;
            }
        }
        // Foreign key
        public int ModuleId { get; set; }
        public Module? Module { get; set; }

    }
}
